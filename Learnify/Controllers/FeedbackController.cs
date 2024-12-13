using Data.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Learnify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository feedbackRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository)
        {
            this.feedbackRepository = feedbackRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = feedbackRepository.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedbackRepository.Add(feedback);
                feedbackRepository.Commit();

                return Created();
            }

            return BadRequest(feedback);
        }

        [HttpPut]
        public IActionResult Edit(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedbackRepository.Edit(feedback);
                feedbackRepository.Commit();

                return Ok(feedback);
            }

            return BadRequest(feedback);
        }

        [HttpDelete]
        public IActionResult Delete(int feedbackId)
        {
            var feedback = feedbackRepository.GetOne(expression: e => e.FeedbackId == feedbackId);
            if (feedback != null)
            {
                feedbackRepository.Delete(feedback);
                feedbackRepository.Commit();
                return Ok(feedback);
            }
            else
                return NotFound();
        }
    }
}
