using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Quiz
    {
        public int QuizId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Range(1, 100, ErrorMessage = "MaxScore must be between 1 and 100.")]
        public int MaxScore { get; set; }

        public ICollection<LessonQuiz> LessonQuizzes { get; set; } // Many-to-Many with Lesson
        public ICollection<UserQuiz> UserQuizzes { get; set; } // Many-to-Many with User
        public ICollection<InstructorQuiz> InstructorQuizzes { get; set; }

    }
}
