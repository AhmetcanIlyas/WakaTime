﻿namespace WakaTime_API.Dtos.UsersDtos
{
    public class UpdateUserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}
