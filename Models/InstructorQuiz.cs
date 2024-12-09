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
    [PrimaryKey(nameof(InstructorId), nameof(QuizId))]
    public class InstructorQuiz
    {
        public int InstructorId { get; set; }
        [ForeignKey(nameof(InstructorId))]
        [ValidateNever]
        public Instructor Instructor { get; set; }

        public int QuizId { get; set; }
        [ForeignKey(nameof(QuizId))]
        [ValidateNever]
        public Quiz Quiz { get; set; }
    }
}
