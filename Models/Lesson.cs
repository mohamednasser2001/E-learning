using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Lesson
    {
        public int LessonId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Content cannot exceed 500 characters.")]
        public string Content { get; set; }

        public ICollection<LessonVideo> LessonVideos { get; set; } // Many-to-Many with Video
        public ICollection<LessonQuiz> LessonQuizzes { get; set; } // Many-to-Many with Quiz
        public ICollection<Assignment> Assignments { get; set; } // One-to-Many with Assignment

    }
}
