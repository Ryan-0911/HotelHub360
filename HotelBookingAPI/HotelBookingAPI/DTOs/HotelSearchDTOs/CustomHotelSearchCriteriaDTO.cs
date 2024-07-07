﻿using HotelBookingAPI.CustomValidator;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.DTOs.HotelSearchDTOs
{
    /// <summary>
    /// This DTO will contain the information whenever we need to search the Hotels based on custom search criteria such as Price Range, Hotel Type, and Amenity Type.
    /// </summary>
    public class CustomHotelSearchCriteriaDTO
    {
        [Range(0, double.MaxValue, ErrorMessage = "Minimum price must be greater than or equal to 0.")]
        public decimal? MinPrice { get; set; }
        [PriceRangeValidation("MinPrice", "MaxPrice")]
        public decimal? MaxPrice { get; set; }
        [StringLength(50, ErrorMessage = "Room type name length cannot exceed 50 characters.")]
        public string? RoomTypeName { get; set; }
        [StringLength(100, ErrorMessage = "Amenity name length cannot exceed 100 characters.")]
        public string? AmenityName { get; set; }
        [StringLength(50, ErrorMessage = "View type name length cannot exceed 50 characters.")]
        public string? ViewType { get; set; }
    }
}
