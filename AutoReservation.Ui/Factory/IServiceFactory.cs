using CarReservation.Common.Interfaces;

namespace CarReservation.Ui.Factory
{
    public interface IServiceFactory
    {
        ICarReservationService GetService();
    }
}