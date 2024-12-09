using System.ComponentModel.DataAnnotations;

namespace Learnify.DTO
{
    public class LessonDTO
    {
        public int LessonId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Content cannot exceed 500 characters.")]
        public string Content { get; set; }
    }
}
