using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<CourseInstructor> CourseInstructors { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<UserQuiz> UserQuizzes { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonVideo> LessonVideos { get; set; }
        public DbSet<LessonQuiz> LessonQuizzes { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<UserAssignment> UserAssignments { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<UserWishlist> UserWishlists { get; set; }
        public DbSet<CourseWishlist> CourseWishlists { get; set; }
        public DbSet<InstructorQuiz> InstructorQuizzes { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CourseFeedback> courseFeedbacks { get; set; }
        public DbSet<CourseUser> courseUsers { get; set; }
    }

}
