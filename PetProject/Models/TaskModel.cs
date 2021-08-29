using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetProject.Models
{
    public class TaskModel
    {
        [Key]
        public int TaskModelId { get; set; }
        [Required]
        [Display(Name ="Task")]
        public string GoalTask { get; set; }
        [Required]
        public string Description { get; set; }
        //[Required]
        //public int ExecutionPriority { get; set; }
        [Display(Name = "Created in")]
        public DateTime? CreatedIn { get; set; } = DateTime.UtcNow;
        [Display(Name = "Edited at")]
        public DateTime? EditedAt { get; set; }
        [Required]
        [Display(Name = "Finish before")]
        public DateTime? FinishBefore { get; set; }
        [Display(Name = "Was edited")]
        public bool IsEdited { get; set; } = false;
    }
}
