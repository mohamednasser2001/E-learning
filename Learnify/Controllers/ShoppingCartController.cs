using Data.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Stripe.Checkout;
using System.Linq.Expressions;

namespace Learnify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ICartRepository cartRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ShoppingCartController(ICartRepository cartRepository, UserManager<ApplicationUser> userManager)
        {
            this.cartRepository = cartRepository;
            this.userManager = userManager;
        }

        [HttpPost("AddToCart")]
        public IActionResult AddToCart(int count, int courseId)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            // Check if the cart item already exists
            var existingCart = cartRepository.GetOne(
                includeProp: null,
                expression: e => e.ApplicationUserId == userId && e.CourseId == courseId
            );

            if (existingCart != null)
            {
                existingCart.Count += count;
                cartRepository.Commit();
                return Ok(existingCart);
            }

            // Add new cart item
            var cart = new Cart
            {
                Count = count,
                CourseId = courseId,
                ApplicationUserId = userId
            };

            cartRepository.Add(cart);
            cartRepository.Commit();

            return Ok(cart);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            var cartItems = cartRepository.GetAll(
                includeProp: new Expression<Func<Cart, object>>[] { e => e.course },
                expression: e => e.ApplicationUserId == userId
            ).ToList();

            return Ok(cartItems);
        }

        [HttpPut("Increment")]
        public IActionResult Increment(int courseId)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            var cartItem = cartRepository.GetOne(
                includeProp: null,
                expression: e => e.ApplicationUserId == userId && e.CourseId == courseId
            );

            if (cartItem == null)
                return NotFound();

            cartItem.Count++;
            cartRepository.Commit();

            return Ok(cartItem);
        }

        [HttpPut("Decrement")]
        public IActionResult Decrement(int courseId)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            var cartItem = cartRepository.GetOne(
                includeProp: null,
                expression: e => e.ApplicationUserId == userId && e.CourseId == courseId
            );

            if (cartItem == null)
                return NotFound();

            cartItem.Count--;

            if (cartItem.Count > 0)
            {
                cartRepository.Commit();
                return Ok(cartItem);
            }
            else
            {
                cartRepository.Delete(cartItem);
                cartRepository.Commit();
                return Ok("Item removed from cart.");
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int courseId)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            var cartItem = cartRepository.GetOne(
                includeProp: null,
                expression: e => e.ApplicationUserId == userId && e.CourseId == courseId
            );

            if (cartItem == null)
                return NotFound();

            cartRepository.Delete(cartItem);
            cartRepository.Commit();

            return Ok("Item removed from cart.");
        }

        [HttpPost("Pay")]
        public IActionResult Pay()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            var cartItems = cartRepository.GetAll(
                includeProp: new Expression<Func<Cart, object>>[] { e => e.course },
                expression: e => e.ApplicationUserId == userId
            ).ToList();

            if (!cartItems.Any())
                return BadRequest("No items in the cart to process payment.");

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/checkout/success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
            };

            foreach (var item in cartItems)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.course.Title
                        },
                        UnitAmount = (long)(item.course.Price * 100) // Convert price to cents
                    },
                    Quantity = item.Count,
                });
            }

            var service = new SessionService();
            var session = service.Create(options);

            return Created(session.Url, cartItems);
        }

    }
}
