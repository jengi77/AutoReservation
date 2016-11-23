using CarReservation.Common.DataTransferObjects;
using CarReservation.Ui.Factory;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CarReservation.Ui.ViewModels
{
    public class CarViewModel : ViewModelBase
    {
        private readonly ObservableCollection<CarDto> cars = new ObservableCollection<CarDto>();

        public CarViewModel(IServiceFactory factory) : base(factory)
        {
            
        }

        public ObservableCollection<CarDto> Cars
        {
            get { return cars; }
        }

        private CarDto selectedCar;
        public CarDto SelectedCar
        {
            get { return selectedCar; }
            set
            {
                if (selectedCar != value)
                {
                    selectedCar = value;
                    OnPropertyChanged(nameof(SelectedCar));
                }
            }
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
            Cars.Clear();
            foreach (var car in Service.Cars)
            {
                Cars.Add(car);
            }
            SelectedCar = Cars.FirstOrDefault();
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
            foreach (var car in Cars)
            {
                if (car.Id == default(int))
                {
                    Service.InsertCar(car);
                }
                else
                {
                    Service.UpdateCar(car);
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

            return Validate(Cars);
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
            Cars.Add(new CarDto());
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
            Service.DeleteCar(SelectedCar);
            Load();
        }

        private bool CanDelete()
        {
            return
                ServiceExists &&
                SelectedCar != null &&
                SelectedCar.Id != default(int);
        }

        #endregion

    }
}