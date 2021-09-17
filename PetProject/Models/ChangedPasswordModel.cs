using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class ChangedPasswordModel
    {
        [Required, Display(Name = "Current password"), DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required, Display(Name = "New password"), DataType(DataType.Password)]
        public string ChangedPassword { get; set; }
        [Required, Compare("ChangedPassword", ErrorMessage = "Passwords doesn't match"), Display(Name = "Confirmed new password"), DataType(DataType.Password)]
        public string ConfirmedChangedPassword { get; set; }
    }
}
