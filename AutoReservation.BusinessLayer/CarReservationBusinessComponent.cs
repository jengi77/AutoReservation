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

        public Reservation GetReservationById(int id)
        {
            using (CarReservationContext context = new CarReservationContext())
            {
                return context.Reservations.Include(r => r.Customer).Include(r => r.Car).SingleOrDefault(r => r.ReservationNo == id );
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
                    int indexEntity = 0;
                    int indexObj = 0;
                    
                    if (entity == null)
                        context.Entry<T>(obj).State = EntityState.Added;
                    if (entity != null && entity.GetType().Name.IndexOf('_') > 0)
                        indexEntity = entity.GetType().Name.IndexOf('_');
                    if (obj.GetType().Name.IndexOf('_') > 0)
                        indexObj = obj.GetType().Name.IndexOf('_');
                    if (entity != null && entity.GetType() == obj.GetType())
                        context.Entry<T>(entity).CurrentValues.SetValues(obj);
                    else if (entity != null && indexEntity != 0 && entity.GetType().Name.Substring(0, indexEntity) == obj.GetType().Name)
                        context.Entry<T>(entity).CurrentValues.SetValues(obj);
                    else if (entity != null && indexEntity != 0 && indexObj != 0 && entity.GetType().Name.Substring(0, indexEntity) ==  obj.GetType().Name.Substring(0, indexObj))
                        context.Entry<T>(entity).CurrentValues.SetValues(obj);
                    else if (context.Entry<T>(obj).State != EntityState.Added)
                    {
                        DeleteObject(obj);
                        SaveObject<T>(obj, 0, isNew);
                    }

                    context.SaveChanges();
                    return obj;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.SingleOrDefault();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    context.SaveChanges();
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