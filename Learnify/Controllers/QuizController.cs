using Data.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Learnify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizRepository quizRepository;

        public QuizController(IQuizRepository quizRepository)
        {
            this.quizRepository = quizRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = quizRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("Details")]
        public IActionResult Details(int quizId)
        {
            var quiz = quizRepository.GetOne(expression: e => e.QuizId == quizId);
            if (quiz != null)
                return Ok(quiz);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Create(Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                quizRepository.Add(quiz);
                quizRepository.Commit();

                return Created();
            }

            return BadRequest(quiz);
        }

        [HttpPut]
        public IActionResult Edit(Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                quizRepository.Edit(quiz);
                quizRepository.Commit();

                return Ok(quiz);
            }

            return BadRequest(quiz);
        }

        [HttpDelete]
        public IActionResult Delete(int quizId)
        {
            var quiz = quizRepository.GetOne(expression: e => e.QuizId == quizId);
            if (quiz != null)
            {
                quizRepository.Delete(quiz);
                quizRepository.Commit();
                return Ok(quiz);
            }
            else
                return NotFound();
        }
    }
}
