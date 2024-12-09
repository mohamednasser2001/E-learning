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
    [PrimaryKey(nameof(ApplicationUserId), nameof(WishlistId))]
    public class UserWishlist
    {
        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        [ValidateNever]
        public int WishlistId { get; set; }
        [ForeignKey(nameof(WishlistId))]
        [ValidateNever]
        public Wishlist Wishlist { get; set; }
    }
}
