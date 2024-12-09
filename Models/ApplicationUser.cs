using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string City { get; set; }
        public string ProfilePictureUrl { get; set; }
        public ICollection<UserAssignment> UserAssignments { get; set; } // Many-to-Many with Assignment
        public ICollection<UserQuiz> UserQuizzes { get; set; } // Many-to-Many with Quiz
        public ICollection<UserWishlist> UserWishlists { get; set; } // Many-to-Many with Wishlist
        public ICollection<Feedback> Feedbacks { get; set; } // One-to-Many with Feedback
        public ICollection<CourseUser> CourseUsers { get; set; } // Many-to-Many with Course
    }
}
