﻿namespace HotelBookingAPI.DTOs.User
{
    public class CreateUserResponseDTO
    {
        public int UserId { get; set; }
        public string Message { get; set; }
        public bool IsCreated { get; set; }
    }
}
