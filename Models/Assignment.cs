using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public ICollection<UserAssignment> UserAssignments { get; set; } // Many-to-Many with User

        [Required]
        public int LessonId { get; set; } // One-to-Many with Lesson
        public Lesson Lesson { get; set; }
    }
}
