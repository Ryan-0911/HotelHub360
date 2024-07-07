using HotelBookingAPI.DTOs.HotelSearchDTOs;
using HotelBookingAPI.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HotelBookingAPI.Repositories
{
    public class HotelSearchRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;
        public HotelSearchRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Retrieves rooms available during the specified check-in and check-out dates, ensuring they don’t overlap with existing reservations except those canceled.
        /// </summary>
        /// <param name="checkInDate"></param>
        /// <param name="checkOutDate"></param>
        /// <returns></returns>
        public async Task<List<RoomSearchDTO>> SearchByAvailabilityAsync(DateTime checkInDate, DateTime checkOutDate)
        {
            var rooms = new List<RoomSearchDTO>();
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var command = new SqlCommand("spSearchByAvailability", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CheckInDate", checkInDate));
                    command.Parameters.Add(new SqlParameter("@CheckOutDate", checkOutDate));
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            rooms.Add(CreateRoomSearchDTO(reader));
                        }
                    }
                }
            }
            return rooms;
        }

        /// <summary>
        /// Finds rooms within a specified price range, helping users find accommodations that fit their budget.
        /// </summary>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <returns></returns>
        public async Task<List<RoomSearchDTO>> SearchByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var rooms = new List<RoomSearchDTO>();
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var command = new SqlCommand("spSearchByPriceRange", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@MinPrice", minPrice));
                    command.Parameters.Add(new SqlParameter("@MaxPrice", maxPrice));
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            rooms.Add(CreateRoomSearchDTO(reader));
                        }
                    }
                }
            }
            return rooms;
        }

        /// <summary>
        /// Filters and retrieves rooms based on a specific room type name, facilitating user searches for rooms that meet particular type requirements.
        /// </summary>
        /// <param name="roomTypeName"></param>
        /// <returns></returns>
        public async Task<List<RoomSearchDTO>> SearchByRoomTypeAsync(string roomTypeName)
        {
            var rooms = new List<RoomSearchDTO>();
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var command = new SqlCommand("spSearchByRoomType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@RoomTypeName", roomTypeName));
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            rooms.Add(CreateRoomSearchDTO(reader));
                        }
                    }
                }
            }
            return rooms;
        }

        /// <summary>
        /// Selects and lists rooms based on their view type (e.g., sea, city), catering to preferences for specific scenic outlooks.
        /// </summary>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public async Task<List<RoomSearchDTO>> SearchByViewTypeAsync(string viewType)
        {
            var rooms = new List<RoomSearchDTO>();
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var command = new SqlCommand("spSearchByViewType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@ViewType", viewType));
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            rooms.Add(CreateRoomSearchDTO(reader));
                        }
                    }
                }
            }
            return rooms;
        }

        /// <summary>
        /// Identifies and lists rooms equipped with certain specified amenities, aiding in the customization of user preferences for room features.
        /// </summary>
        /// <param name="amenityName"></param>
        /// <returns></returns>
        public async Task<List<RoomSearchDTO>> SearchByAmenitiesAsync(string amenityName)
        {
            var rooms = new List<RoomSearchDTO>();
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var command = new SqlCommand("spSearchByAmenities", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@AmenityName", amenityName));
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            rooms.Add(CreateRoomSearchDTO(reader));
                        }
                    }
                }
            }
            return rooms;
        }

        /// <summary>
        /// Retrieves all rooms associated with a given room type ID, useful for detailed and specific queries related to room categorization.
        /// </summary>
        /// <param name="roomTypeID"></param>
        /// <returns></returns>
        public async Task<List<RoomSearchDTO>> SearchRoomsByRoomTypeIDAsync(int roomTypeID)
        {
            var rooms = new List<RoomSearchDTO>();
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var command = new SqlCommand("spSearchRoomsByRoomTypeID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@RoomTypeID", roomTypeID));
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            rooms.Add(CreateRoomSearchDTO(reader));
                        }
                    }
                }
            }
            return rooms;
        }

        /// <summary>
        /// This method provides comprehensive details about a specific room, including information about the room type and the amenities it offers. Thus, it gives a full overview for guest decision-making or administrative purposes.
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public async Task<RoomDetailsWithAmenitiesSearchDTO> GetRoomDetailsWithAmenitiesByRoomIDAsync(int roomID)
        {
            RoomDetailsWithAmenitiesSearchDTO roomDetails = new RoomDetailsWithAmenitiesSearchDTO();
            //List<AmenitySearchDTO> amenities = new List<AmenitySearchDTO>();
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var command = new SqlCommand("spGetRoomDetailsWithAmenitiesByRoomID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@RoomID", roomID));
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            roomDetails.Room = CreateRoomSearchDTO(reader);
                            roomDetails.Amenities = new List<AmenitySearchDTO>();
                            if (await reader.NextResultAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    roomDetails.Amenities.Add(new AmenitySearchDTO
                                    {
                                        AmenityID = reader.GetInt32(reader.GetOrdinal("AmenityID")),
                                        Name = reader.GetString(reader.GetOrdinal("Name")),
                                        Description = reader.GetString(reader.GetOrdinal("Description"))
                                    });
                                }
                            }
                        }
                    }
                }
            }
            return roomDetails;
        }

        /// <summary>
        /// This function fetches all amenities available in a specified room, which helps provide detailed amenities information to guests or for service quality assessments.
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public async Task<List<AmenitySearchDTO>> GetRoomAmenitiesByRoomIDAsync(int roomID)
        {
            var amenities = new List<AmenitySearchDTO>();
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var command = new SqlCommand("spGetRoomAmenitiesByRoomID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@RoomID", roomID));
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            amenities.Add(new AmenitySearchDTO
                            {
                                AmenityID = reader.GetInt32(reader.GetOrdinal("AmenityID")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Description = reader.GetValueByColumn<string>("Description")
                            });
                        }
                    }
                }
            }
            return amenities;
        }

        /// <summary>
        /// This method filters and returns rooms that have a minimum average guest rating, assisting in ensuring quality standards or preferences are met.
        /// </summary>
        /// <param name="minRating"></param>
        /// <returns></returns>
        public async Task<List<RoomSearchDTO>> SearchByMinRatingAsync(float minRating)
        {
            var rooms = new List<RoomSearchDTO>();
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var command = new SqlCommand("spSearchByMinRating", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@MinRating", minRating));
                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                rooms.Add(CreateRoomSearchDTO(reader));
                            }
                        }
                    }
                }
            }
            return rooms;
        }

        /// <summary>
        /// This function conducts a flexible search based on multiple optional criteria, such as price, room type, amenities, and view type, using a dynamic approach to match various user-defined filters.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async Task<List<RoomSearchDTO>> SearchCustomCombinationAsync(CustomHotelSearchCriteriaDTO criteria)
        {
            var rooms = new List<RoomSearchDTO>();
            using (var connection = _connectionFactory.CreateConnection())
            {
                var command = new SqlCommand("spSearchCustomCombination", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@MinPrice", (object)criteria.MinPrice ?? DBNull.Value);
                command.Parameters.AddWithValue("@MaxPrice", (object)criteria.MaxPrice ?? DBNull.Value);
                command.Parameters.AddWithValue("@RoomTypeName", string.IsNullOrEmpty(criteria.RoomTypeName) ? DBNull.Value : criteria.RoomTypeName);
                command.Parameters.AddWithValue("@AmenityName", string.IsNullOrEmpty(criteria.AmenityName) ? DBNull.Value : criteria.AmenityName);
                command.Parameters.AddWithValue("@ViewType", string.IsNullOrEmpty(criteria.ViewType) ? DBNull.Value : criteria.ViewType);
                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        rooms.Add(CreateRoomSearchDTO(reader));
                    }
                }
            }
            return rooms;
        }

        private RoomSearchDTO CreateRoomSearchDTO(SqlDataReader reader)
        {
            return new RoomSearchDTO
            {
                RoomID = reader.GetInt32(reader.GetOrdinal("RoomID")),
                RoomNumber = reader.GetString(reader.GetOrdinal("RoomNumber")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                BedType = reader.GetString(reader.GetOrdinal("BedType")),
                ViewType = reader.GetString(reader.GetOrdinal("ViewType")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                RoomType = new RoomTypeSearchDTO
                {
                    RoomTypeID = reader.GetInt32(reader.GetOrdinal("RoomTypeID")),
                    TypeName = reader.GetString(reader.GetOrdinal("TypeName")),
                    AccessibilityFeatures = reader.GetString(reader.GetOrdinal("AccessibilityFeatures")),
                    Description = reader.GetString(reader.GetOrdinal("Description"))
                }
            };
        }
    }
}
