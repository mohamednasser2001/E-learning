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
    [PrimaryKey(nameof(LessonId), nameof(QuizId))]
    public class LessonQuiz
    {
        public int LessonId { get; set; }
        [ForeignKey(nameof(LessonId))]
        [ValidateNever]
        public Lesson Lesson { get; set; }

        public int QuizId { get; set; }
        [ForeignKey(nameof(QuizId))]
        [ValidateNever]
        public Quiz Quiz { get; set; }
    }
}
