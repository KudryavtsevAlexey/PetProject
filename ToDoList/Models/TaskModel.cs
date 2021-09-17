using System;
using System.ComponentModel.DataAnnotations;
using ToDoList.Entities;

namespace ToDoList.Models
{
    public class TaskModel
    {
        [Key]
        public int TaskModelId { get; set; }
        [Required]
        [Display(Name ="Task")]
        [StringLength(50, MinimumLength = 3)]
        public string GoalTask { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string Description { get; set; }
        [Required]
        [Range(1,10)]
        [Display(Name ="Execution Priority (1 - the most important)")]
        public int? ExecutionPriority { get; set; }
        [Display(Name = "Created in")]
        public DateTime? CreatedIn { get; set; } = DateTime.UtcNow;
        [Display(Name = "Edited at")]
        public DateTime? EditedAt { get; set; } = DateTime.UtcNow;
        [Required]
        [Display(Name = "Finish before")]
        public DateTime? FinishBefore { get; set; }
        [Display(Name = "Was edited")]
        public bool IsEdited { get; set; } = false;
        
        public ApplicationUser ApplicationUser { get; set; }
        public int ApplicationUserOfTasksId { get; set; }
        
    }
}
