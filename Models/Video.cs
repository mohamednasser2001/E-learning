using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Video
    {
        public int VideoId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string Url { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        public ICollection<LessonVideo> LessonVideos { get; set; } // Many-to-Many with Lesson

    }
}
