using CarReservation.Common.DataTransferObjects;
using CarReservation.Ui.Factory;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CarReservation.Ui.ViewModels;

namespace CarReservation.Ui.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        private readonly ObservableCollection<CustomerDto> customers = new ObservableCollection<CustomerDto>();

        public CustomerViewModel(IServiceFactory factory) : base(factory)
        {
            
        }

        public ObservableCollection<CustomerDto> Customers
        {
            get { return customers; }
        }

        private CustomerDto selectedCustomer;
        public CustomerDto SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                if (selectedCustomer != value)
                {
                    selectedCustomer = value;
                    OnPropertyChanged(nameof(SelectedCustomer));
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
            Customers.Clear();
            foreach (var Customer in Service.Customers)
            {
                Customers.Add(Customer);
            }
            SelectedCustomer = Customers.FirstOrDefault();
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
            foreach (var customer in Customers)
            {
                if (customer.Id == default(int))
                {
                    Service.InsertCustomer(customer);
                }
                else
                {
                    var x = Service.UpdateCustomer(customer);
                    x = null;
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

            return Validate(Customers);
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
            Customers.Add(new CustomerDto { Birthday = DateTime.Today });
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
            Service.DeleteCustomer(SelectedCustomer);
            Load();
        }

        private bool CanDelete()
        {
            return
                ServiceExists &&
                SelectedCustomer != null &&
                SelectedCustomer.Id != default(int);
        }

        #endregion

    }
}
