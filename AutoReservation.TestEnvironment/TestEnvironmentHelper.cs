using CarReservation.Dal;
using CarReservation.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace CarReservation.TestEnvironment
{
    public static class TestEnvironmentHelper
    {
        public static void InitializeTestData()
        {
            using (CarReservationContext context = new CarReservationContext())
            {
                var luxuryCarTableName = context.GetTableName<LuxuryCar>();
                var midRangeCarTableName = context.GetTableName<MidRangeCar>();
                var standardCarTableName = context.GetTableName<StandardCar>();
                var carTableName = context.GetTableName<Car>();
                var customerTableName = context.GetTableName<Customer>();
                var reservationTableName = context.GetTableName<Reservation>();

                try
                {
                    context.Database.Log = Console.Write;
                    // Delete all records from tables
                    //      > Cleanup for specific subtypes necessary when not using table per hierarchy (TPH)
                    //        since entities will be stored in different tables.
                    if (luxuryCarTableName != carTableName)
                    { 
                        context.DeleteAllRecords(luxuryCarTableName); 
                    }
                    if (midRangeCarTableName != carTableName)
                    { 
                        context.DeleteAllRecords(midRangeCarTableName); 
                    }
                    if (standardCarTableName != carTableName)
                    { 
                        context.DeleteAllRecords(standardCarTableName); 
                    }
                    context.DeleteAllRecords(reservationTableName);
                    context.DeleteAllRecords(carTableName);
                    context.DeleteAllRecords(customerTableName);

                    // Reset the identity seed (Id's will start again from 1)
                    context.ResetEntitySeed(luxuryCarTableName);
                    context.ResetEntitySeed(midRangeCarTableName);
                    context.ResetEntitySeed(standardCarTableName);
                    context.ResetEntitySeed(carTableName);
                    context.ResetEntitySeed(customerTableName);
                    context.ResetEntitySeed(reservationTableName);

                    // Temporarily allow insertion of identity columns (Id)
                    context.SetAutoIncrementOnTable(luxuryCarTableName, true);
                    context.SetAutoIncrementOnTable(midRangeCarTableName, true);
                    context.SetAutoIncrementOnTable(standardCarTableName, true);
                    context.SetAutoIncrementOnTable(carTableName, true);
                    context.SetAutoIncrementOnTable(customerTableName, true);
                    context.SetAutoIncrementOnTable(reservationTableName, true);

                    // Insert test data
                    context.Cars.AddRange(Cars);
                    context.Customers.AddRange(Customers);
                    context.Reservations.AddRange(Reservations);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Error while re-initializing database entries.", ex);
                }
                finally
                {
                    // Disable insertion of identity columns (Id)
                    context.SetAutoIncrementOnTable(luxuryCarTableName, false);
                    context.SetAutoIncrementOnTable(midRangeCarTableName, false);
                    context.SetAutoIncrementOnTable(standardCarTableName, false);
                    context.SetAutoIncrementOnTable(carTableName, false);
                    context.SetAutoIncrementOnTable(customerTableName, false);
                    context.SetAutoIncrementOnTable(reservationTableName, false);
                }
            }
        }

        private static List<Customer> Customers =>
            new List<Customer>
            {
                new Customer {Id = 1, Lastname = "Nass", Firstname = "Anna", Birthday = new DateTime(1981, 05, 05)},
                new Customer {Id = 2, Lastname = "Beil", Firstname = "Timo", Birthday = new DateTime(1980, 09, 09)},
                new Customer {Id = 3, Lastname = "Pfahl", Firstname = "Martha", Birthday = new DateTime(1990, 07, 03)},
                new Customer {Id = 4, Lastname = "Zufall", Firstname = "Rainer", Birthday = new DateTime(1954, 11, 11)},
            };

        private static List<Car> Cars =>
            new List<Car>
            {
                new StandardCar {Id = 1, Brand = "Fiat Punto", DailyRate = 50},
                new MidRangeCar {Id = 2, Brand = "VW Golf", DailyRate = 120},
                new LuxuryCar {Id = 3, Brand = "Audi S6", DailyRate = 180, BaseRate = 50},
            };

        private static List<Reservation> Reservations =>
            new List<Reservation>
            {
                new Reservation { ReservationNo = 1, CarId = 1, CustomerId = 1, From = new DateTime(2020, 01, 10), To = new DateTime(2020, 01, 20)},
                new Reservation { ReservationNo = 2, CarId = 2, CustomerId = 2, From = new DateTime(2020, 01, 10), To = new DateTime(2020, 01, 20)},
                new Reservation { ReservationNo = 3, CarId = 3, CustomerId = 3, From = new DateTime(2020, 01, 10), To = new DateTime(2020, 01, 20)},
            };

        private static string GetTableName<T>(this DbContext context)
        {
            // See: https://lowrymedia.com/2014/06/10/ef6-1-mapping-between-types-tables-including-derived-types/
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;
            var objectItemCollection = (ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace);
            var entityTypeClr = metadata.GetItems<EntityType>(DataSpace.OSpace).Single(e => objectItemCollection.GetClrType(e) == typeof(T));
            var entityTypeCSpace = metadata.GetItems(DataSpace.CSpace).Where(x => x.BuiltInTypeKind == BuiltInTypeKind.EntityType).Cast<EntityType>().Single(x => x.Name == entityTypeClr.Name);
            var mappingsCSSpace = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace).Single().EntitySetMappings.ToList();

            EntitySet entitySet;
            var mapping = mappingsCSSpace.SingleOrDefault(x => x.EntitySet.Name == entityTypeCSpace.Name);
            if (mapping != null)
            {
                entitySet = mapping.EntityTypeMappings.Single().Fragments.Single().StoreEntitySet;
            }
            else
            {
                mapping = mappingsCSSpace.SingleOrDefault(x => x.EntityTypeMappings.Where(y => y.EntityType != null).Any(y => y.EntityType.Name == entityTypeCSpace.Name));
                if (mapping != null)
                {
                    entitySet = mapping.EntityTypeMappings.Where(x => x.EntityType != null).Single(x => x.EntityType.Name == entityTypeClr.Name).Fragments.Single().StoreEntitySet;
                }
                else
                {
                    var entitySetMapping = mappingsCSSpace.Single(x => x.EntityTypeMappings.Any(y => y.IsOfEntityTypes.Any(z => z.Name == entityTypeCSpace.Name)));
                    entitySet = entitySetMapping.EntityTypeMappings.First(x => x.IsOfEntityTypes.Any(y => y.Name == entityTypeCSpace.Name)).Fragments.Single().StoreEntitySet;
                }
            }

            string schema = (string)entitySet.MetadataProperties["Schema"].Value ?? entitySet.Schema;
            string table = (string)entitySet.MetadataProperties["Table"].Value ?? entitySet.Name;

            return $"[{schema}].[{table}]";
        }

        private static void DeleteAllRecords(this DbContext context, string table)
        {
            context.Database.ExecuteSqlCommand($"DELETE FROM {table}");
        }

        private static void ResetEntitySeed(this DbContext context, string table)
        {
            if (context.TableHasIdentityColumn(table))
            {
                context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('{table}', RESEED, 0)");
            }
        }

        private static void SetAutoIncrementOnTable(this DbContext context, string table, bool isAutoIncrementOn)
        {
            if (context.TableHasIdentityColumn(table))
            {
                context.Database.ExecuteSqlCommand($"SET IDENTITY_INSERT {table} {(isAutoIncrementOn ? "ON" : "OFF")}");
            }
        }

        private static bool TableHasIdentityColumn(this DbContext context, string table)
        {
            return context.Database.SqlQuery<int>($"SELECT OBJECTPROPERTY(OBJECT_ID('{table}'), 'TableHasIdentity')").Single() == 1;
        }

    }
}