namespace CarReservation.Ui.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly CarViewModel carViewModel;
        private readonly CustomerViewModel customerViewModel;
        private readonly ReservationViewModel reservationViewModel;

        public MainWindowViewModel(CarViewModel carViewModel, CustomerViewModel customerViewModel,
            ReservationViewModel reservationViewModel)
        {
            this.carViewModel = carViewModel;
            this.customerViewModel = customerViewModel;
            this.reservationViewModel = reservationViewModel;
        }

        public void Init()
        {
            carViewModel.Init();
            customerViewModel.Init();
            reservationViewModel.Init();
        }

        public CarViewModel CarViewModel
        {
            get { return carViewModel; }
        }

        public CustomerViewModel CustomerViewModel
        {
            get { return customerViewModel; }
        }

        public ReservationViewModel ReservationViewModel
        {
            get { return reservationViewModel; }
        }
    }
}
