using System.ComponentModel.DataAnnotations;

namespace CarReservation.Dal.Entities
{
    public class LuxuryCar : Car
    {
        [Required]
        public int BaseRate { get; set; }
    }
}
