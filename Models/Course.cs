using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Course
    {
        public int CourseId { get; set; } 

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public int Price { get; set; }

        [Required(ErrorMessage = "CreateDate is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
        public DateTime CreateDate { get; set; }

        public ICollection<CourseInstructor> courseInstructors { get; set; }
        public ICollection<CourseFeedback> courseFeedbacks { get; set; }
        public ICollection<CourseUser> CourseUsers { get; set; } // Many-to-Many with Course
        public ICollection<CourseWishlist> CourseWishlists { get; set; } // Many-to-Many with Wishlist
        public ICollection<Lesson> Lessons { get; set; }

    }
}
