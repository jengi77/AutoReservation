using System.ComponentModel.DataAnnotations;

namespace AutoReservation.Dal.Entities
{
    public class LuxuryCar : Car
    {
        [Required]
        public int BaseRate { get; set; }
    }
}
