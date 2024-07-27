﻿using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models.Users
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Your Password is limited to {2} to {1} characters", MinimumLength = 6)]
        public string Password { get; set; }

    }
}
