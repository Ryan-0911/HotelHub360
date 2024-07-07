using HotelBookingAPI.DTOs.HotelSearchDTOs;
using HotelBookingAPI.Models;
using HotelBookingAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelSearchController : ControllerBase
    {
        private readonly HotelSearchRepository _hotelSearchRepository;
        private readonly ILogger<HotelSearchController> _logger;
        public HotelSearchController(HotelSearchRepository hotelSearchRepository, ILogger<HotelSearchController> logger)
        {
            _hotelSearchRepository = hotelSearchRepository;
            _logger = logger;
        }

        /// <summary>
        /// This endpoint Handles requests to find rooms available within a specified check-in and check-out date range, providing users with options that match their stay duration without any reservation conflicts. (e.g., checkInDate=2024-05-15、checkOutDate=2024-05-18) 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("Availability")]

        public async Task<APIResponse<List<RoomSearchDTO>>> SearchByAvailability([FromQuery] AvailabilityHotelSearchRequestDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("Invalid Data in the Request Body");
                    return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "Invalid Data in the Request Body");
                }
                var rooms = await _hotelSearchRepository.SearchByAvailabilityAsync(request.CheckInDate, request.CheckOutDate);
                if (rooms != null && rooms.Count > 0)
                {
                    return new APIResponse<List<RoomSearchDTO>>(rooms, "Fetch Available Room Successful");
                }
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "No Record Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get available rooms");
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.InternalServerError, "Failed to get available rooms", ex.Message);
            }
        }

        /// <summary>
        /// Manages requests to find rooms within a specific price range, helping users find accommodations within their budget constraints.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("PriceRange")]
        public async Task<APIResponse<List<RoomSearchDTO>>> SearchByPriceRange([FromQuery] PriceRangeHotelSearchRequestDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("Invalid Price Range in the Request Body");
                    return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "Invalid Data in the Request Body");
                }
                var rooms = await _hotelSearchRepository.SearchByPriceRangeAsync(request.MinPrice, request.MaxPrice);
                if (rooms != null && rooms.Count > 0)
                {
                    return new APIResponse<List<RoomSearchDTO>>(rooms, "Fetch rooms by price range Successful");
                }
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "No Record Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get rooms by price range");
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.InternalServerError, "An error occurred while fetching rooms by price range.", ex.Message);
            }
        }

        /// <summary>
        /// This endpoint handles requests to filter rooms based on a specified room type name, allowing users to select rooms that meet specific configuration or feature requirements.
        /// </summary>
        /// <param name="roomTypeName"></param>
        /// <returns></returns>
        [HttpGet("RoomType")]
        public async Task<APIResponse<List<RoomSearchDTO>>> SearchByRoomType(string roomTypeName)
        {
            try
            {
                if (string.IsNullOrEmpty(roomTypeName))
                {
                    _logger.LogInformation("Room Type Name is Empty");
                    return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "Room Type Name is Empty");
                }
                var rooms = await _hotelSearchRepository.SearchByRoomTypeAsync(roomTypeName);
                if (rooms != null && rooms.Count > 0)
                {
                    return new APIResponse<List<RoomSearchDTO>>(rooms, "Fetch rooms by room type Successful");
                }
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "No Record Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get rooms by room type");
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.InternalServerError, "An error occurred while fetching rooms by room type.", ex.Message);
            }
        }

        /// <summary>
        /// Handles requests for rooms with a particular type of view, like sea or city, catering to preferences for specific types of scenic vistas from the room.
        /// </summary>
        /// <param name="viewType"></param>
        /// <returns></returns>
        [HttpGet("ViewType")]
        public async Task<APIResponse<List<RoomSearchDTO>>> SearchByViewType(string viewType)
        {
            try
            {
                if (string.IsNullOrEmpty(viewType))
                {
                    _logger.LogInformation("View Type is Empty");
                    return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "View Type is Empty");
                }
                var rooms = await _hotelSearchRepository.SearchByViewTypeAsync(viewType);
                if (rooms != null && rooms.Count > 0)
                {
                    return new APIResponse<List<RoomSearchDTO>>(rooms, "Fetch rooms by view type Successful");
                }
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "No Record Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get rooms by view type");
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.InternalServerError, "An error occurred while fetching rooms by view type.", ex.Message);
            }
        }

        /// <summary>
        /// This endpoint manages requests to find rooms equipped with specified amenities, allowing users to choose rooms with specific desirable features, such as spas or gyms.
        /// </summary>
        /// <param name="amenityName"></param>
        /// <returns></returns>
        [HttpGet("Amenities")]
        public async Task<APIResponse<List<RoomSearchDTO>>> SearchByAmenities(string amenityName)
        {
            try
            {
                if (string.IsNullOrEmpty(amenityName))
                {
                    _logger.LogInformation("Amenity Name is Empty");
                    return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "Amenity Name is Empty");
                }
                var rooms = await _hotelSearchRepository.SearchByAmenitiesAsync(amenityName);
                if (rooms != null && rooms.Count > 0)
                {
                    return new APIResponse<List<RoomSearchDTO>>(rooms, "Fetch rooms by amenities Successful");
                }
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "No Record Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get rooms by amenities");
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.InternalServerError, "An error occurred while fetching rooms by amenities.", ex.Message);
            }
        }

        /// <summary>
        /// This endpoint handles requests to retrieve all rooms associated with a specific room type ID. It is useful for users looking for detailed listings based on room categorizations.
        /// </summary>
        /// <param name="roomTypeID"></param>
        /// <returns></returns>
        [HttpGet("RoomsByType")]
        public async Task<APIResponse<List<RoomSearchDTO>>> SearchRoomsByRoomTypeID(int roomTypeID)
        {
            try
            {
                if (roomTypeID <= 0)
                {
                    _logger.LogInformation($"Invalid Room Type ID, {roomTypeID}");
                    return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, $"Invalid Room Type ID, {roomTypeID}");
                }
                var rooms = await _hotelSearchRepository.SearchRoomsByRoomTypeIDAsync(roomTypeID);
                if (rooms != null && rooms.Count > 0)
                {
                    return new APIResponse<List<RoomSearchDTO>>(rooms, "Fetch rooms by room type ID Successful");
                }
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "No Record Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get rooms by room type ID");
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.InternalServerError, "An error occurred while fetching rooms by room type ID.", ex.Message);
            }
        }

        /// <summary>
        /// Manages detailed information requests for a specific room, including its amenities, thus offering a comprehensive overview for guest decision-making.
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>
        [HttpGet("RoomDetails")]
        public async Task<APIResponse<RoomDetailsWithAmenitiesSearchDTO>> GetRoomDetailsWithAmenitiesByRoomID(int roomID)
        {
            try
            {
                if (roomID <= 0)
                {
                    _logger.LogInformation($"Invalid Room ID, {roomID}");
                    return new APIResponse<RoomDetailsWithAmenitiesSearchDTO>(HttpStatusCode.BadRequest, $"Invalid Room ID, {roomID}");
                }
                var roomDetails = await _hotelSearchRepository.GetRoomDetailsWithAmenitiesByRoomIDAsync(roomID);
                if (roomDetails != null)
                    return new APIResponse<RoomDetailsWithAmenitiesSearchDTO>(roomDetails, "Fetch room details with amenities for RoomID Successful");
                else
                    return new APIResponse<RoomDetailsWithAmenitiesSearchDTO>(HttpStatusCode.BadRequest, "No Record Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get room details with amenities for RoomID {roomID}");
                return new APIResponse<RoomDetailsWithAmenitiesSearchDTO>(HttpStatusCode.InternalServerError, "An error occurred while fetching room details with amenities.", ex.Message);
            }
        }

        /// <summary>
        /// This endpoint handles requests to fetch a detailed list of amenities for a specific room, aiding users in understanding what features are included in their potential accommodation.
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>
        [HttpGet("RoomAmenities")]
        public async Task<APIResponse<List<AmenitySearchDTO>>> GetRoomAmenitiesByRoomID(int roomID)
        {
            try
            {
                if (roomID <= 0)
                {
                    _logger.LogInformation($"Invalid Room ID, {roomID}");
                    return new APIResponse<List<AmenitySearchDTO>>(HttpStatusCode.BadRequest, $"Invalid Room ID, {roomID}");
                }
                var amenities = await _hotelSearchRepository.GetRoomAmenitiesByRoomIDAsync(roomID);
                if (amenities != null && amenities.Count > 0)
                {
                    return new APIResponse<List<AmenitySearchDTO>>(amenities, "Fetch Amenities for RoomID Successful");
                }
                return new APIResponse<List<AmenitySearchDTO>>(HttpStatusCode.BadRequest, "No Record Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get amenities for RoomID {roomID}");
                return new APIResponse<List<AmenitySearchDTO>>(HttpStatusCode.InternalServerError, "An error occurred while fetching room amenities.", ex.Message);
            }
        }

        /// <summary>
        /// Manages requests to filter rooms based on a minimum average guest rating, ensuring users can select rooms that uphold a certain quality standard.
        /// </summary>
        /// <param name="minRating"></param>
        /// <returns></returns>
        [HttpGet("ByRating")]
        public async Task<APIResponse<List<RoomSearchDTO>>> SearchByMinRating(float minRating)
        {
            try
            {
                if (minRating <= 0 && minRating > 5)
                {
                    _logger.LogInformation($"Invalid Rating: {minRating}");
                    return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, $"Invalid Rating: {minRating}");
                }
                var rooms = await _hotelSearchRepository.SearchByMinRatingAsync(minRating);
                if (rooms != null && rooms.Count > 0)
                {
                    return new APIResponse<List<RoomSearchDTO>>(rooms, "Fetch rooms by minimum rating Successful");
                }
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "No Record Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get rooms by minimum rating");
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.InternalServerError, "An error occurred while fetching rooms by minimum rating.", ex.Message);
            }
        }

        /// <summary>
        /// Handles flexible search queries based on a combination of criteria such as price, room type, amenities, and view type, offering a dynamic search capability that caters to varied user preferences.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpGet("CustomSearch")]
        public async Task<APIResponse<List<RoomSearchDTO>>> SearchCustomCombination([FromQuery] CustomHotelSearchCriteriaDTO criteria)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("Invalid Data in the Request Body");
                    return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "Invalid Data in the Request Body");
                }
                var rooms = await _hotelSearchRepository.SearchCustomCombinationAsync(criteria);
                if (rooms != null && rooms.Count > 0)
                {
                    return new APIResponse<List<RoomSearchDTO>>(rooms, "Fetch Room By Custom Search Successful");
                }
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.BadRequest, "No Record Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to perform custom search");
                return new APIResponse<List<RoomSearchDTO>>(HttpStatusCode.InternalServerError, "An error occurred during the custom search.", ex.Message);
            }
        }
    }
}
