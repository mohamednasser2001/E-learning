using Data.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ICourseRepository courseRepository;

        public HomeController(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = courseRepository.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("Details")]
        public IActionResult Details(int courseId)
        {
            var result = courseRepository.GetOne(expression: e => e.CourseId == courseId);

            if (result != null)
                return Ok(result);

            return NotFound();
        }
    }
}
