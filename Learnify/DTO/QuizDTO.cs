using System.ComponentModel.DataAnnotations;

namespace Learnify.DTO
{
    public class QuizDTO
    {
        public int QuizId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Range(1, 100, ErrorMessage = "MaxScore must be between 1 and 100.")]
        public int MaxScore { get; set; }
    }
}
