using Data.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Stripe.Checkout;

namespace Learnify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private ICartRepository cartRepository;
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
            Cart cart = new Cart
            {
                Count = count,
                CourseId = courseId,
                ApplicationUserId = userId
            };

            cartRepository.Create(cart);
            cartRepository.Commit();

            return Ok(cart);
        }
        (object, bool) f()
        {
            return ("fds", true);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = userManager.GetUserId(User);
            var cartItems = cartRepository.GetAll(
                includeProperties: new[] { "Course" },
                filter: obj => (obj).ApplicationUserId == userId
            ).ToList();

            return Ok(cartItems);
        }

        [HttpPut("Increment")]
        public IActionResult Increment(int courseId)
        {
            var userId = userManager.GetUserId(User);

            var cartItem = cartRepository.GetOne(e => e.ApplicationUserId == userId && e.CourseId == courseId);
            if (cartItem != null)
            {
                cartItem.Count++;
                cartRepository.Commit();

                return Ok(cartItem);
            }

            return NotFound();
        }

        [HttpPut("Decrement")]
        public IActionResult Decrement(int courseId)
        {
            var userId = userManager.GetUserId(User);

            var cartItem = cartRepository.GetOne(e => e.ApplicationUserId == userId && e.CourseId == courseId);
            if (cartItem != null)
            {
                cartItem.Count--;
                if (cartItem.Count > 0)
                {
                    cartRepository.Commit();
                }
                else
                {
                    cartItem.Count = 1;
                }

                return Ok(cartItem);
            }

            return NotFound();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int courseId)
        {
            var userId = userManager.GetUserId(User);

            var cartItem = cartRepository.GetOne(e => e.ApplicationUserId == userId && e.CourseId == courseId);
            if (cartItem != null)
            {
                cartRepository.Delete(cartItem);
                cartRepository.Commit();

                return Ok(cartItem);
            }

            return NotFound();
        }

        [HttpPost("Pay")]
        public IActionResult Pay()
        {
            var userId = userManager.GetUserId(User);

            var cartItems = cartRepository.GetAll(
                includeProperties: new[] { "Course" },
                filter: e => e.ApplicationUserId == userId
            ).ToList();

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
                        UnitAmount = (long)((GetCourse(item).Price * 100))
                    },
                    Quantity = item.Count,
                });
            }

            var service = new SessionService();
            var session = service.Create(options);

            return Created(session.Url, cartItems);
        }

        private static Course GetCourse(Cart item)
        {
            return item.course;
        }
    }
}
