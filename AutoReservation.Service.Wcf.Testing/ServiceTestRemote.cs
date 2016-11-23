using CarReservation.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;

namespace CarReservation.Service.Wcf.Testing
{
    [TestClass]
    public class ServiceTestRemote : ServiceTestBase
    {
        private static ServiceHost serviceHost;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            serviceHost = new ServiceHost(typeof(CarReservationService));
            serviceHost.Open();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            if (serviceHost.State != CommunicationState.Closed)
            {
                serviceHost.Close();
            }
        }

        private ICarReservationService target;
        protected override ICarReservationService Target
        {
            get
            {
                if (target == null)
                {
                    ChannelFactory<ICarReservationService> channelFactory = new ChannelFactory<ICarReservationService>("CarReservationService");
                    target = channelFactory.CreateChannel();
                }
                return target;
            }
        }

    }
}