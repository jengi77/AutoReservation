using System;
using System.Collections.Generic;
using CarReservation.Dal;
using CarReservation.Dal.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace CarReservation.BusinessLayer
{
    public class CarReservationBusinessComponent
    {
        private static LocalOptimisticConcurrencyException<T> CreateLocalOptimisticConcurrencyException<T>(CarReservationContext context, T entity)
            where T : class
        {
            var dbEntity = (T)context.Entry(entity)
                .GetDatabaseValues()
                .ToObject();

            return new LocalOptimisticConcurrencyException<T>($"Update {typeof(Car).Name}: Concurrency-Fehler", dbEntity);
        }

        public List<T> GetAll<T>() where T: class
        {
            using (CarReservationContext context = new CarReservationContext())
            {
               return context.Set<T>().ToList();
            }  
        }

        public List<Reservation> GetAllReservation()
        {
            using (CarReservationContext context = new CarReservationContext())
            {
                return context.Reservations.Include(r => r.Customer).Include(r => r.Car).ToList();
            }
        }

        public T GetElement<T>(Func<T, bool> predicateFunc ) where T : class
        {
            using (CarReservationContext context = new CarReservationContext())
            {
                return context.Set<T>().SingleOrDefault(predicateFunc );
            }
        }

        public T SaveObject<T>(T obj,int id, bool isNew) where T: class
        {
            using (CarReservationContext context = new CarReservationContext())
            {
                context.Database.Log = Console.Write;
                try
                {
                    var entity = context.Set<T>().Find(id);
                    if (entity == null)
                        context.Entry<T>(obj).State = EntityState.Added;
                    else if (entity.GetType() == obj.GetType())
                        context.Entry<T>(entity).CurrentValues.SetValues(obj);
                    else
                    {
                        DeleteObject(obj);
                        SaveObject<T>(obj, 0, isNew);
                    }

                    context.SaveChanges();
                    return obj;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw CreateLocalOptimisticConcurrencyException(context, obj);
                }
            }
        }

        public void DeleteObject<T>(T obj) where T : class
        {
            using (CarReservationContext context = new CarReservationContext())
            {
                context.Entry<T>(obj).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}