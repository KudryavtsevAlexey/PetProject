using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PetProject.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required,Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required, Display(Name = "Date of birth")]
        public DateTime? DateOfBirth { get; set; }
    }
}
