using CarReservation.Ui.ViewModels;
using Ninject;
using System.Windows;
using CarReservation.Ui;

namespace CarReservation.Ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel kernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            kernel = LoadNinject();

            var viewModel = kernel.Get<MainWindowViewModel>();
            viewModel.Init();

            MainWindow = new MainWindow(viewModel);
            MainWindow.Show();
        }

        private IKernel LoadNinject()
        {
            var kernel = new StandardKernel(new CarReservationModule());
            kernel.Load("AutoReservation.Ui.Factory.NinjectBindings.xml");
            return kernel;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            kernel.Dispose();
            base.OnExit(e);
        }
    }
}
