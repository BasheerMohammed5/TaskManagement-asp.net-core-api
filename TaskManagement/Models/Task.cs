using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        [Required]
        public required string Title { get; set; }

        public required string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}