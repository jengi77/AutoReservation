using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
    public abstract class Car
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(20)]
        public string Marque { get; set; }
        [Required]
        public int DailyRate { get; set; }
        [Required, Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
