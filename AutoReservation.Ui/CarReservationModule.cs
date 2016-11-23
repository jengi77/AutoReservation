using CarReservation.Ui.ViewModels;
using Ninject.Modules;

namespace CarReservation.Ui
{
    public class CarReservationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<MainWindowViewModel>().ToSelf();
            Bind<CarViewModel>().ToSelf();
            Bind<CustomerViewModel>().ToSelf();
            Bind<ReservationViewModel>().ToSelf();
        }
    }
}
