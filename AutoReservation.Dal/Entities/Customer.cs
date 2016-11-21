using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarReservation.Dal.Entities
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        [Required, StringLength(20)]
        public string Lastname { get; set; }
        [Required, StringLength(20)]
        public string Firstname { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }
        [Required, Timestamp]
        public byte[] RowVersion { get; set; }
        [InverseProperty("Customers")]
        public ICollection<Reservation> Reservations { get; set; }
    }
}
