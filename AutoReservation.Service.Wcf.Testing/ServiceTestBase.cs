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
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void GetCutomersTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void GetReservationenTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion

        #region Get by existing ID

        [TestMethod]
        public void GetCarByIdTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void GetCustomerByIdTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion

        #region Get by not existing ID

        [TestMethod]
        public void GetCarByIdWithIllegalIdTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void GetCustomerByIdWithIllegalIdTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void GetReservationByNrWithIllegalIdTest()
        {
        }

        #endregion

        #region Insert

        [TestMethod]
        public void InsertCarTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void InsertCustomerTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion

        #region Delete  

        [TestMethod]
        public void DeleteCarTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void DeleteCustomerTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion

        #region Update

        [TestMethod]
        public void UpdateCarTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateCustomerTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion

        #region Update with optimistic concurrency violation

        [TestMethod]
        public void UpdateCarWithOptimisticConcurrencyTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateCustomerWithOptimisticConcurrencyTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion
    }
}
