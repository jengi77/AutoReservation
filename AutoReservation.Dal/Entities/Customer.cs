using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarReservation.Dal.Entities
{
    [Table("customers")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(20)]
        public string Lastname { get; set; }
        [Required, StringLength(20)]
        public string Firstname { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [InverseProperty("Customer")]
        public ICollection<Reservation> Reservations { get; set; }
    }
}
