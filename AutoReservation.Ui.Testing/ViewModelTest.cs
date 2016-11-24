using CarReservation.TestEnvironment;
using CarReservation.Ui.Factory;
using CarReservation.Ui.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Windows.Input;

namespace CarReservation.Ui.Testing
{
    [TestClass]
    public class ViewModelTest
    {
        private IKernel kernel;

        [TestInitialize]
        public void InitializeTestData()
        {
            kernel = new StandardKernel();
            kernel.Load("AutoReservation.Ui.Factory.NinjectBindings.xml");

            TestEnvironmentHelper.InitializeTestData();
        }
        
        [TestMethod]
        public void CarsLoadTest()
        {
            CarViewModel vm = new CarViewModel(kernel.Get<IServiceFactory>());
            vm.Init();

            ICommand targetCommand = vm.LoadCommand;

            Assert.IsTrue(targetCommand.CanExecute(null));

            targetCommand.Execute(null);

            Assert.AreEqual(3, vm.Cars.Count);
        }

        [TestMethod]
        public void CustomersLoadTest()
        {
            CustomerViewModel vm = new CustomerViewModel(kernel.Get<IServiceFactory>());
            vm.Init();

            ICommand targetCommand = vm.LoadCommand;

            Assert.IsTrue(targetCommand.CanExecute(null));

            targetCommand.Execute(null);

            Assert.AreEqual(4, vm.Customers.Count);
        }

        [TestMethod]
        public void ReservationenLoadTest()
        {
            ReservationViewModel vm = new ReservationViewModel(kernel.Get<IServiceFactory>());
            vm.Init();

            ICommand targetCommand = vm.LoadCommand;

            Assert.IsTrue(targetCommand.CanExecute(null));

            targetCommand.Execute(null);

            Assert.AreEqual(3, vm.Reservations.Count);
        }
    }
}