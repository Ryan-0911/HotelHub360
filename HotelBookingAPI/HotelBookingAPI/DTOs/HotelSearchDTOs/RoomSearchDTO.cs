namespace HotelBookingAPI.DTOs.HotelSearchDTOs
{
    /// <summary>
    /// This DTO will contain all the room information and the corresponding room type information. 
    /// In most of the cases we will use this DTO to return the search result.
    /// </summary>
    public class RoomSearchDTO
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string BedType { get; set; }
        public string ViewType { get; set; }
        public string Status { get; set; }
        public RoomTypeSearchDTO RoomType { get; set; }
    }
}
