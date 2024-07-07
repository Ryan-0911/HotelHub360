using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.Room
{
    public class CreateRoomTypeDTO
    {
        [Required]
        public string TypeName { get; set; }
        public string AccessibilityFeatures { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
