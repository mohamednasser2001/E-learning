using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [PrimaryKey(nameof(LessonId), nameof(VideoId))]
    public class LessonVideo
    {
        public int LessonId { get; set; }
        [ForeignKey(nameof(LessonId))]
        [ValidateNever]
        public Lesson Lesson { get; set; }

        public int VideoId { get; set; }
        [ForeignKey(nameof(VideoId))]
        [ValidateNever]
        public Video Video { get; set; }
    }
}
