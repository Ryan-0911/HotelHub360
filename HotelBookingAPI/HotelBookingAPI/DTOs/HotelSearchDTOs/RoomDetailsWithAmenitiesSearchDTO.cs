namespace HotelBookingAPI.DTOs.HotelSearchDTOs
{
    /// <summary>
    /// We will use the following DTO whenever we fetch a particular Room’s Details. This will include the Room Information, Room Type information, and available Amenities.
    /// </summary>
    public class RoomDetailsWithAmenitiesSearchDTO
    {
        public RoomSearchDTO Room { get; set; }
        public List<AmenitySearchDTO> Amenities { get; set; }
    }
}
