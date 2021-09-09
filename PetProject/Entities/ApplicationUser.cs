using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PetProject.Models;

namespace PetProject.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Tasks = new List<TaskModel>();
        }

        [Required,Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required, Display(Name = "Date of birth")]
        public DateTime? DateOfBirth { get; set; }
        public List<TaskModel> Tasks { get; set; }
    }
}
