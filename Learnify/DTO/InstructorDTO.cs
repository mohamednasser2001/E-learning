using System.ComponentModel.DataAnnotations;

namespace Learnify.DTO
{
    public class InstructorDTO
    {
        public int InstructorId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Bio cannot exceed 1000 characters.")]
        public string Bio { get; set; }

        [Required(ErrorMessage = "Expertise is required.")]
        [StringLength(200, ErrorMessage = "Expertise cannot exceed 200 characters.")]
        public string Expertise { get; set; }
    }
}
