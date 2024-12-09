using Data.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Learnify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonRepository lessonRepository;

        public LessonController(ILessonRepository lessonRepository)
        {
            this.lessonRepository = lessonRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = lessonRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("Details")]
        public IActionResult Details(int lessonId)
        {
            var lesson = lessonRepository.GetOne(expression: e => e.LessonId == lessonId);
            if (lesson != null)
                return Ok(lesson);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Create(Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                lessonRepository.Create(lesson);
                lessonRepository.Commit();

                return Created();
            }

            return BadRequest(lesson);
        }

        [HttpPut]
        public IActionResult Edit(Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                lessonRepository.Edit(lesson);
                lessonRepository.Commit();

                return Ok(lesson);
            }

            return BadRequest(lesson);
        }

        [HttpDelete]
        public IActionResult Delete(int lessonId)
        {
            var lesson = lessonRepository.GetOne(expression: e => e.LessonId == lessonId);
            if (lesson != null)
            {
                lessonRepository.Delete(lesson);
                lessonRepository.Commit();
                return Ok(lesson);
            }
            else
                return NotFound();
        }
    }
}
