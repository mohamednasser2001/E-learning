using System.ComponentModel.DataAnnotations;

namespace Learnify.DTO
{
    public class LoginDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememperMe { get; set; }
    }
}
