using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.IRepository;
using Models;

namespace Data.Repository
{
   public class WishlistRepository:Repository<Wishlist>,IWishlistRepository
    {
        public WishlistRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
