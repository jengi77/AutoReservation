using System;
using CarReservation.Dal.Entities;
using CarReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Car tempCar = Target.GetElement<Car>(c => c.Id == 1);
            tempCar.Brand = "Test_Brand";
            Target.SaveObject(tempCar,tempCar.Id, false);
            Assert.AreEqual(Target.GetElement<Car>(c => c.Id == 1).Brand, "Test_Brand");
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            Customer tempCustomer = Target.GetElement<Customer>(c => c.Id == 1);
            tempCustomer.Lastname = "Test_Lastname";
            Target.SaveObject(tempCustomer, tempCustomer.Id, false);
            Assert.AreEqual(Target.GetElement<Customer>(c => c.Id == 1).Lastname, "Test_Lastname");
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            DateTime date = DateTime.Now;
            Reservation tempReservation = Target.GetReservationById(1);
            tempReservation.From = date;
            Target.SaveObject(tempReservation, tempReservation.ReservationNo, false);
            Assert.AreEqual(Target.GetReservationById(1).From.ToString(), date.ToString());
        }

    }

}
