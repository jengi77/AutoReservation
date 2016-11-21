using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarReservation.Dal.Entities
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Id"), InverseProperty("Reservations")]
        public Car Car { get; set; }
        [ForeignKey("Id"), InverseProperty("Reservations")]
        public Customer Customer { get; set; }
        [DataType(DataType.Date)]
        public DateTime From { get; set; }
        [DataType(DataType.Date)]
        public DateTime To { get; set; }
    }
}
