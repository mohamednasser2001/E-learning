using System.ComponentModel.DataAnnotations;

namespace Learnify.DTO
{
    public class AssignmentDTO
    {
        public int AssignmentId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }
}
