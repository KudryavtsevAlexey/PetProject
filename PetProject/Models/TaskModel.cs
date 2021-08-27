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
        public string GoalTask { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime? CreatedIn { get; set; } = DateTime.UtcNow;
        public DateTime? EditedAt { get; set; }
        [Required]
        public DateTime? FinishBefore { get; set; }
        public bool IsEdited { get; set; } = false;
    }
}
