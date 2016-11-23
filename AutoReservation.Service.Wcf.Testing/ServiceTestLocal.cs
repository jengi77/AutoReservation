using CarReservation.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarReservation.Service.Wcf.Testing
{
    [TestClass]
    public class ServiceTestLocal : ServiceTestBase
    {

        private ICarReservationService target;
        protected override ICarReservationService Target
        {
            get
            {
                if (target == null)
                {
                    target = new CarReservationService();
                }
                return target;
            }
        }

    }
}