namespace HotelBookingAPI.DTOs.HotelSearchDTOs
{
    /// <summary>
    /// This DTO will contain the Room Type information, which is basically used within other DTOs to return the Room Type information.
    /// </summary>
    public class RoomTypeSearchDTO
    {
        public int RoomTypeID { get; set; }
        public string TypeName { get; set; }
        public string AccessibilityFeatures { get; set; }
        public string Description { get; set; }
    }
}
