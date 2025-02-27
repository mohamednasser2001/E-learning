﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [PrimaryKey(nameof(ApplicationUserId), nameof(QuizId))]
    public class UserQuiz
    {

        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }


        public int QuizId { get; set; }
        [ForeignKey(nameof(QuizId))]
        [ValidateNever]
        public Quiz Quiz { get; set; }
    }
}
