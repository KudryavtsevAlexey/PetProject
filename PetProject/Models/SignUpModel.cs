using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PetProject.Entities;

namespace PetProject.Models
{
    public class SignUpModel
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, Compare("Password", ErrorMessage = "Passwords mismatch"), Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
