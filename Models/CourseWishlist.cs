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
    [PrimaryKey(nameof(CourseId), nameof(WishlistId))]
    public class CourseWishlist
    {
        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        [ValidateNever]
        public Course Course { get; set; }
        public int WishlistId { get; set; }
        [ForeignKey(nameof(WishlistId))]
        [ValidateNever]
        public Wishlist Wishlist { get; set; }
    }
}
