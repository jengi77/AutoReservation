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
            Target.SaveObject(tempCar,tempCar.RowVersion, false);
            Assert.AreEqual(Target.GetElement<Car>(c => c.Id == 1).Brand, "Test_Brand");
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            Customer tempCustomer = Target.GetElement<Customer>(c => c.Id == 1);
            tempCustomer.Lastname = "Test_Lastname";
            Target.SaveObject(tempCustomer, tempCustomer.RowVersion, false);
            Assert.AreEqual(Target.GetElement<Customer>(c => c.Id == 1).Lastname, "Test_Lastname");
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            DateTime date = new DateTime();
            Reservation tempReservation = Target.GetElement<Reservation>(c => c.ReservationNo == 1);
            tempReservation.From = date;
            Target.SaveObject(tempReservation, tempReservation.RowVersion, false);
            Assert.AreEqual(Target.GetElement<Reservation>(c => c.ReservationNo == 1).From, date);
        }

    }

}
