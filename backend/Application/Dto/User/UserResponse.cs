﻿using Business.Models;

namespace Application.Dto.User
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool ShowMyContact { get; set; }
    }
}
