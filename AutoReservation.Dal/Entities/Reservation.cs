using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarReservation.Dal.Entities
{
    [Table("reservations")]
    public class Reservation
    {
        [Key, Column("id")]
        public int ReservationNo { get; set; }
        [Column("car")]
        public int CarId { get; set; }
        [ForeignKey("CarId"), InverseProperty("Reservations")]
        public virtual Car Car { get; set; }
        [Column("customer")]
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId"), InverseProperty("Reservations")]
        public virtual Customer Customer { get; set; }
        [DataType(DataType.Date)]
        public DateTime From { get; set; }
        [DataType(DataType.Date)]
        public DateTime To { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
