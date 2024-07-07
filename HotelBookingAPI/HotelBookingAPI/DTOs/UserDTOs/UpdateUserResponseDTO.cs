namespace HotelBookingAPI.DTOs.User
{
    public class UpdateUserResponseDTO
    {
        public int UserId { get; set; }
        public string Message { get; set; }
        public bool IsUpdated { get; set; }
    }
}
