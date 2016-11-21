using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarReservation.Dal.Entities
{
    public abstract class Car
    {
        [Key]
        public Guid Id { get; set; }
        [Required, StringLength(20)]
        public string Marque { get; set; }
        [Required]
        public int DailyRate { get; set; }
        [Required, Timestamp]
        public byte[] RowVersion { get; set; }
        [InverseProperty("Cars")]
        public ICollection<Reservation> Reservations { get; set; }
    }
}
