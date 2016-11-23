using CarReservation.Common.Interfaces;
using CarReservation.Service.Wcf;

namespace CarReservation.Ui.Factory
{
    public class LocalDataAccessServiceFactory : IServiceFactory
    {
        public ICarReservationService GetService()
        {
            return new CarReservationService();
        }
    }
}
