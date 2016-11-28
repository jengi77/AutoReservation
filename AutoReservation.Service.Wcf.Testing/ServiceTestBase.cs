using CarReservation.Common.DataTransferObjects;
using CarReservation.Common.Interfaces;
using CarReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace CarReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract ICarReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        #region Read all entities

        [TestMethod]
        public void GetCarsTest()
        {
            Assert.AreEqual(3, Target.Cars.Count);
        }

        [TestMethod]
        public void GetCustomersTest()
        {
            Assert.AreEqual(4, Target.Customers.Count);
        }

        [TestMethod]
        public void GetReservationenTest()
        {
            Assert.AreEqual(3, Target.Reservations.Count);
        }

        #endregion

        #region Get by existing ID

        [TestMethod]
        public void GetCarByIdTest()
        {
            Assert.AreEqual("Fiat Punto", Target.GetCarById(1).Brand);
        }

        [TestMethod]
        public void GetCustomerByIdTest()
        {
            Assert.AreEqual("Anna", Target.GetCustomerById(1).Firstname);
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            Assert.AreEqual(new DateTime(2020, 01, 10), Target.GetReservationByNr(1).From);
        }

        #endregion

        #region Get by not existing ID

        [TestMethod]
        public void GetCarByIdWithIllegalIdTest()
        {
            Assert.AreEqual(null, Target.GetCarById(7));
        }

        [TestMethod]
        public void GetCustomerByIdWithIllegalIdTest()
        {
            Assert.AreEqual(null, Target.GetCustomerById(7));
        }

        [TestMethod]
        public void GetReservationByNrWithIllegalIdTest()
        {
            Assert.AreEqual(null, Target.GetReservationByNr(7));
        }

        #endregion

        #region Insert

        [TestMethod]
        public void InsertCarTest()
        {
            CarDto carDto = new CarDto
            {
                Id = 0,
                Brand = "Test",
                DailyRate = 40,
                CarClass = CarClass.MidRange
            };
            Target.InsertCar(carDto);
            Assert.AreEqual(4, Target.Cars.Count);
        }

        [TestMethod]
        public void InsertCustomerTest()
        {
            CustomerDto customerDto = new CustomerDto
            {
                Id = 0,
                Firstname = "Gustav",
                Lastname = "Gugus",
                Birthday = new DateTime(1992, 07, 22)
            };
            Target.InsertCustomer(customerDto);
            Assert.AreEqual(5, Target.Customers.Count);
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            ReservationDto reservationDto = new ReservationDto
            {
                ReservationNo = 0,
                From = new DateTime(2016, 11, 12),
                To = new DateTime(2016, 11, 24),
                Car = Target.GetCarById(1),
                Customer = Target.GetCustomerById(1)
            };
            Target.InsertReservation(reservationDto);
            Assert.AreEqual(4, Target.Reservations.Count);
        }

        #endregion

        #region Delete  

        [TestMethod]
        public void DeleteCarTest()
        {
            CarDto tempCar = Target.GetCarById(1);
            Target.DeleteCar(tempCar);
            Assert.AreEqual(2, Target.Cars.Count);
        }

        [TestMethod]
        public void DeleteCustomerTest()
        {
            CustomerDto tempCustomerDto = Target.GetCustomerById(1);
            Target.DeleteCustomer(tempCustomerDto);
            Assert.AreEqual(3, Target.Customers.Count);
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            ReservationDto tempReservationDto = Target.GetReservationByNr(1);
            Target.DeleteReservation(tempReservationDto);
            Assert.AreEqual(2, Target.Reservations.Count);
        }

        #endregion

        #region Update

        [TestMethod]
        public void UpdateCarTest()
        {
            CarDto tempCar = Target.GetCarById(1);
            tempCar.Brand = "Test_Brand";
            Target.UpdateCar(tempCar);
            Assert.AreEqual(Target.GetCarById(1).Brand, "Test_Brand");
        }

        [TestMethod]
        public void UpdateCustomerTest()
        {
            CustomerDto tempCustomerDto = Target.GetCustomerById(1);
            tempCustomerDto.Firstname = "Test_User";
            Target.UpdateCustomer(tempCustomerDto);
            Assert.AreEqual(Target.GetCustomerById(1).Firstname, "Test_User");
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            ReservationDto reservationDto = Target.GetReservationByNr(1);
            reservationDto.From = new DateTime(2017, 01, 01);
            Target.UpdateReservation(reservationDto);
            
            Assert.AreEqual(new DateTime(2017,01,01), Target.GetReservationByNr(1).From);
        }

        #endregion

        #region Update with optimistic concurrency violation

        [TestMethod]
        public void UpdateCarWithOptimisticConcurrencyTest()
        {
            CarDto firstCar = Target.GetCarById(1);
            firstCar.Brand = "Guguland";
            CarDto secondCar = Target.GetCarById(1);
            secondCar.BaseRate = 40;
            Target.UpdateCar(firstCar);
            Target.UpdateCar(secondCar);
            Assert.Fail("DbOptimisticConcurrencyException");
        }

        [TestMethod]
        public void UpdateCustomerWithOptimisticConcurrencyTest()
        {
            CustomerDto firstCustomer = Target.GetCustomerById(1);
            firstCustomer.Firstname = "Guguland";
            CustomerDto secondCustomer = Target.GetCustomerById(1);
            secondCustomer.Lastname = "TEST2";
            Target.UpdateCustomer(firstCustomer);
            Target.UpdateCustomer(secondCustomer);
            
            Assert.Fail("DbOptimisticConcurrencyException");
        }

        [TestMethod]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            ReservationDto firstReservation = Target.GetReservationByNr(1);
            firstReservation.From = DateTime.Now;
            ReservationDto secondReservation = Target.GetReservationByNr(1);
            secondReservation.To = DateTime.Now;
            Target.UpdateReservation(firstReservation);
            Target.UpdateReservation(secondReservation);
            Assert.Fail("DbOptimisticConcurrencyException");
        }

        #endregion
    }
}
