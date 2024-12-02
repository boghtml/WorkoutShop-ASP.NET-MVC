using Microsoft.AspNetCore.Identity;
using System;

namespace WorkoutShop.Models
{
    public class User : IdentityUser
    {
         public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

