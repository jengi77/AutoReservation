using CarReservation.Dal;
using CarReservation.Dal.Entities;
using CarReservation.BusinessLayer;

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
    }
}