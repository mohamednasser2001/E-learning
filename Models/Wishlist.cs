using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Wishlist
    {
        public int Id { get; set; }
        public string text { get; set; }
        [Required]
        public DateTime AddedAt { get; set; }

        public ICollection<UserWishlist> UserWishlists { get; set; } // Many-to-Many with User
        public ICollection<CourseWishlist> CourseWishlists { get; set; } // Many-to-Many with Course
    }
}
