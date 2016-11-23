using System;
using System.Data.Entity;
using System.Text;
using CarReservation.Common.DataTransferObjects.Core;

namespace CarReservation.Common.DataTransferObjects
{   
    public class CustomerDto : DtoBase<CustomerDto>
    {
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

        private DateTime birthday;

        public DateTime Birthday
        {
            get { return birthday; }
            set
            {
                if (birthday != value)
                {
                    birthday = value;
                    OnPropertyChanged(nameof(Birthday));
                }
            }
        }

        private string firstname;

        public string Firstname
        {
            get { return firstname; }
            set
            {
                if (firstname != value)
                {
                    firstname = value;
                    OnPropertyChanged(nameof(Firstname));
                }
            }
        }

        private string lastname;

        public string Lastname
        {
            get { return lastname; }
            set
            {
                if (lastname != value)
                {
                    lastname = value;
                    OnPropertyChanged(nameof(Lastname));
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
            if (string.IsNullOrEmpty(Lastname))
            {
                error.AppendLine("- Nachname ist nicht gesetzt.");
            }
            if (string.IsNullOrEmpty(Firstname))
            {
                error.AppendLine("- Vorname ist nicht gesetzt.");
            }
            if (Birthday == DateTime.MinValue)
            {
                error.AppendLine("- Geburtsdatum ist nicht gesetzt.");
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override string ToString()
            => $"{Id}; {Lastname}; {Firstname}; {Birthday}";

    }
}
