using System;
using System.ComponentModel.DataAnnotations;

namespace AutoReservation.Dal.Entities
{
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
        [Required, Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
