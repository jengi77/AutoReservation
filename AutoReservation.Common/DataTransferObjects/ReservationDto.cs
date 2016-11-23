
using System;
using System.Text;
using CarReservation.Common.DataTransferObjects.Core;

namespace CarReservation.Common.DataTransferObjects
{
    public class ReservationDto : DtoBase<ReservationDto>
    {
        private int reservationNo;

        public int ReservationNo
        {
            get { return reservationNo; }
            set
            {
                if (reservationNo != value)
                {
                    reservationNo = value;
                    OnPropertyChanged(nameof(ReservationNo));
                }
            }
        }

        private int carId;

        public int CarId
        {
            get { return carId; }
            set
            {
                if (carId != value)
                {
                    carId = value;
                    OnPropertyChanged(nameof(CarId));
                }
            }
        }

        private int customerId;

        public int CustomerId
        {
            get { return customerId; }
            set
            {
                if (customerId != value)
                {
                    customerId = value;
                    OnPropertyChanged(nameof(CustomerId));
                }
            }
        }

        private DateTime to;

        public DateTime To
        {
            get { return to; }
            set
            {
                if (to != value)
                {
                    to = value;
                    OnPropertyChanged(nameof(To));
                }
            }
        }

        private DateTime from;

        public DateTime From
        {
            get { return from; }
            set
            {
                if (from != value)
                {
                    from = value;
                    OnPropertyChanged(nameof(From));
                }
            }
        }

        private byte[] rowVersion;
        public byte[] RowVersion
        {
            get { return rowVersion; }
            set
            {
                if (rowVersion != value)
                {
                    rowVersion = value;
                    OnPropertyChanged(nameof(RowVersion));
                }
            }
        }

        private CarDto car;

        public CarDto Car
        {
            get { return car; }
            set
            {
                if (car != value)
                {
                    car = value;
                    OnPropertyChanged(nameof(Car));
                }
            }
        }

        private CustomerDto customer;

        public CustomerDto Customer
        {
            get { return customer; }
            set
            {
                if (customer != value)
                {
                    customer = value;
                    OnPropertyChanged(nameof(Customer));
                }
            }
        }

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (From == DateTime.MinValue)
            {
                error.AppendLine("- Von-Datum ist nicht gesetzt.");
            }
            if (To == DateTime.MinValue)
            {
                error.AppendLine("- Bis-Datum ist nicht gesetzt.");
            }
            if (From > To)
            {
                error.AppendLine("- Von-Datum ist grösser als Bis-Datum.");
            }
            if (Car == null)
            {
                error.AppendLine("- Auto ist nicht zugewiesen.");
            }
            else
            {
                string autoError = Car.Validate();
                if (!string.IsNullOrEmpty(autoError))
                {
                    error.AppendLine(autoError);
                }
            }
            if (Customer == null)
            {
                error.AppendLine("- Kunde ist nicht zugewiesen.");
            }
            else
            {
                string kundeError = Customer.Validate();
                if (!string.IsNullOrEmpty(kundeError))
                {
                    error.AppendLine(kundeError);
                }
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override string ToString()
            => $"{ReservationNo}; {From}; {To}; {Car}; {Customer}";

    }
}
