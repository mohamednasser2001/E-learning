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
    [PrimaryKey(nameof(CourseId), nameof(InstructorId))]
    public class CourseInstructor
    {
        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        [ValidateNever]
        public Course Course { get; set; }

        public int InstructorId { get; set; }
        [ForeignKey(nameof(InstructorId))]
        [ValidateNever]
        public Instructor Instructor { get; set; }
    }
}
