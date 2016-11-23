using CarReservation.Common.DataTransferObjects;
using CarReservation.Ui.Factory;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CarReservation.Ui.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ReservationDto> reservationen = new ObservableCollection<ReservationDto>();

        public ReservationViewModel(IServiceFactory factory) : base(factory)
        {
            
        }

        public ObservableCollection<ReservationDto> Reservationen
        {
            get { return reservationen; }
        }

        private ReservationDto selectedReservation;
        public ReservationDto SelectedReservation
        {
            get { return selectedReservation; }
            set
            {
                if (selectedReservation != value)
                {
                    selectedReservation = value;
                    SelectedCarId = value?.Car != null ? value.Car.Id : 0;
                    SelectedCustomerId = value?.Customer != null ? value.Customer.Id : 0;

                    OnPropertyChanged(nameof(SelectedReservation));
                }
            }
        }

        private int selectedCarId;
        public int SelectedCarId
        {
            get { return selectedCarId; }
            set
            {
                if (selectedCarId != value)
                {
                    selectedCarId = value;
                    if (SelectedReservation != null)
                    {
                        SelectedReservation.Car = Cars.SingleOrDefault(a => a.Id == value);
                    }

                    OnPropertyChanged(nameof(SelectedCarId));
                }
            }
        }

        private int selectedCustomerId;
        public int SelectedCustomerId
        {
            get { return selectedCustomerId; }
            set
            {
                if (selectedCustomerId != value)
                {
                    selectedCustomerId = value;
                    if (SelectedReservation != null)
                    {
                        SelectedReservation.Customer = Customers.SingleOrDefault(k => k.Id == value);
                    }

                    OnPropertyChanged(nameof(SelectedCustomerId));
                }
            }
        }

        private readonly ObservableCollection<CarDto> cars = new ObservableCollection<CarDto>();
        public ObservableCollection<CarDto> Cars
        {
            get { return cars; }
        }

        private readonly ObservableCollection<CustomerDto> customers = new ObservableCollection<CustomerDto>();
        public ObservableCollection<CustomerDto> Customers
        {
            get { return customers; }
        }

        #region Load-Command

        private RelayCommand loadCommand;

        public ICommand LoadCommand
        {
            get
            {
                return loadCommand ?? (loadCommand = new RelayCommand(param => Load(), param => CanLoad()));
            }
        }

        protected override void Load()
        {
            Reservationen.Clear();
            
            Customers.Clear();
            Cars.Clear();

            foreach (CustomerDto Customer in Service.Customers)
            {
                Customers.Add(Customer);
            }
            foreach (CarDto Car in Service.Cars)
            {
                Cars.Add(Car);
            }
            foreach (ReservationDto reservation in Service.Reservations)
            {
                Reservationen.Add(reservation);
            }
            SelectedReservation = Reservationen.FirstOrDefault();
        }

        private bool CanLoad()
        {
            return ServiceExists;
        }

        #endregion

        #region Save-Command

        private RelayCommand saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(param => SaveData(), param => CanSaveData()));
            }
        }

        private void SaveData()
        {
            foreach (var reservation in Reservationen)
            {
                if (reservation.ReservationNo == default(int))
                {
                    Service.InsertReservation(reservation);
                }
                else
                {
                    Service.UpdateReservation(reservation);
                }
            }
            Load();
        }

        private bool CanSaveData()
        {
            if (!ServiceExists)
            {
                return false;
            }

            return Validate(Reservationen);
        }

        #endregion

        #region New-Command

        private RelayCommand newCommand;

        public ICommand NewCommand
        {
            get
            {
                return newCommand ?? (newCommand = new RelayCommand(param => New(), param => CanNew()));
            }
        }

        private void New()
        {
            Reservationen.Add(new ReservationDto
            {
                From = DateTime.Today,
                To = DateTime.Today
            });
        }

        private bool CanNew()
        {
            return ServiceExists;
        }

        #endregion

        #region Delete-Command

        private RelayCommand deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(param => Delete(), param => CanDelete()));
            }
        }

        private void Delete()
        {
            Service.DeleteReservation(SelectedReservation);
            Load();
        }

        private bool CanDelete()
        {
            return
                ServiceExists &&
                SelectedReservation != null &&
                SelectedReservation.ReservationNo != default(int);
        }

        #endregion

    }
}