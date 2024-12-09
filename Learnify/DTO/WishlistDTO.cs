using System.ComponentModel.DataAnnotations;

namespace Learnify.DTO
{
    public class WishlistDTO
    {
        public int Id { get; set; }
        public string text { get; set; }
        [Required]
        public DateTime AddedAt { get; set; }
    }
}
