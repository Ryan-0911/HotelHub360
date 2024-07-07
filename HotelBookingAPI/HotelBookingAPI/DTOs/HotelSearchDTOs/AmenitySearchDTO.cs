namespace HotelBookingAPI.DTOs.HotelSearchDTOs
{
    /// <summary>
    /// This DTO allows us to return all the Amenities based on a Room ID.
    /// </summary>
    public class AmenitySearchDTO
    {
        public int AmenityID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
