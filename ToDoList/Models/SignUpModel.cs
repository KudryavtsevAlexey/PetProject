using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
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
