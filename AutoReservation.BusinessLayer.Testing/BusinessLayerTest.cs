using CarReservation.Dal.Entities;
using CarReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CarReservation.BusinessLayer.Testing
{
    [TestClass]
    public class BusinessLayerTest
    {

        private CarReservationBusinessComponent target;
        private CarReservationBusinessComponent Target
        {
            get
            {
                if (target == null)
                {
                    target = new CarReservationBusinessComponent();
                }
                return target;
            }
        }
        
        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }
        
        [TestMethod]
        public void UpdateAutoTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

    }

}
