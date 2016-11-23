using CarReservation.Common.Interfaces;
using System.ServiceModel;

namespace CarReservation.Ui.Factory
{
    public class RemoteDataAccessServiceFactory : IServiceFactory
    {
        public ICarReservationService GetService()
        {
            ChannelFactory<ICarReservationService> channelFactory = new ChannelFactory<ICarReservationService>("CarReservationService");
            return channelFactory.CreateChannel();
        }
    }
}
