using System.Data.Entity;
using System.Text;
using CarReservation.Common.DataTransferObjects.Core;

namespace CarReservation.Common.DataTransferObjects
{
    public class CarDto : DtoBase<CarDto>
    {
        private int baseRate;
        public int BaseRate
        {
            get { return baseRate; }
            set
            {
                if (baseRate != value)
                {
                    baseRate = value;
                    OnPropertyChanged(nameof(BaseRate));
                }
            }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string brand;
        public string Brand
        {
            get { return brand; }
            set
            {
                if (brand != value)
                {
                    brand = value;
                    OnPropertyChanged(nameof(Brand));
                }
            }
        }

        private int dailyRate;
        public int DailyRate
        {
            get { return dailyRate; }
            set
            {
                if (dailyRate != value)
                {
                    dailyRate = value;
                    OnPropertyChanged(nameof(DailyRate));
                }
            }
        }

        private CarClass carClass;
        public CarClass CarClass
        {
            get { return carClass; }
            set
            {
                if (carClass != value)
                {
                    carClass = value;
                    OnPropertyChanged(nameof(CarClass));
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

        private DbSet<ReservationDto> reservations;
        public DbSet<ReservationDto> Reservations
        {
            get { return reservations; }
            set
            {
                if (reservations != value)
                {
                    reservations = value;
                    OnPropertyChanged(nameof(Reservations));
                }
            }
        }

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(Brand))
            {
                error.AppendLine("- Marke ist nicht gesetzt.");
            }
            if (DailyRate <= 0)
            {
                error.AppendLine("- Tagestarif muss grösser als 0 sein.");
            }
            if (CarClass == CarClass.Luxury && BaseRate <= 0)
            {
                error.AppendLine("- Basistarif eines Luxusautos muss grösser als 0 sein.");
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override string ToString()
            => $"{Id}; {Brand}; {DailyRate}; {BaseRate}; {CarClass}";

    }
}
