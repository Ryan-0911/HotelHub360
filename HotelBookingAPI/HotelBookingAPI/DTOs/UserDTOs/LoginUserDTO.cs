using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.User
{
    public class LoginUserDTO
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
