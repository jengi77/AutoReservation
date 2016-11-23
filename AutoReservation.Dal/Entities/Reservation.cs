using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarReservation.Dal.Entities
{
    public class Reservation
    {
        [Key, Column("Id")]
        public int ReservationNo { get; set; }
        [Column("Car")]
        public int CarId { get; set; }
        [ForeignKey("Id"), InverseProperty("Reservations")]
        public Car Car { get; set; }
        [ForeignKey("Id"), InverseProperty("Reservations")]
        [Column("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [DataType(DataType.Date)]
        public DateTime From { get; set; }
        [DataType(DataType.Date)]
        public DateTime To { get; set; }
        [Required, Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
