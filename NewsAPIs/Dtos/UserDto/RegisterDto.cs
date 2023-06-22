﻿using System.ComponentModel.DataAnnotations;

namespace NewsAPIs.Models
{
    public class RegisterDto
    {
        [Required , StringLength(100)]
        public string FirstName { get; set; }
        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Required, StringLength(100)]
        public string Username { get; set; }
        [Required, StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
      
        [Required, StringLength(100)]
        public string Password { get; set; }
    }
}
