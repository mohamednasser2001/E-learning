using Data.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Learnify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistRepository wishlistRepository;

        public WishlistController(IWishlistRepository wishlistRepository)
        {
            this.wishlistRepository = wishlistRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = wishlistRepository.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                wishlistRepository.Add(wishlist);
                wishlistRepository.Commit();

                return Created();
            }

            return BadRequest(wishlist);
        }

        [HttpPut]
        public IActionResult Edit(Wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                wishlistRepository.Edit(wishlist);
                wishlistRepository.Commit();

                return Ok(wishlist);
            }

            return BadRequest(wishlist);
        }

        [HttpDelete]
        public IActionResult Delete(int wishlistId)
        {
            var wishlist = wishlistRepository.GetOne(expression: e => e.Id == wishlistId);
            if (wishlist != null)
            {
                wishlistRepository.Delete(wishlist);
                wishlistRepository.Commit();
                return Ok(wishlist);
            }
            else
                return NotFound();
        }
    }
}
