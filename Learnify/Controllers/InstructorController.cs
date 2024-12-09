using Data.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Learnify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorRepository instructorRepository;

        public InstructorController(IInstructorRepository instructorRepository)
        {
            this.instructorRepository = instructorRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = instructorRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("Details")]
        public IActionResult Details(int instructorId)
        {
            var instructor = instructorRepository.GetOne(expression: e => e.InstructorId == instructorId);
            if (instructor != null)
                return Ok(instructor);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                instructorRepository.Create(instructor);
                instructorRepository.Commit();

                return Created();
            }

            return BadRequest(instructor);
        }

        [HttpPut]
        public IActionResult Edit(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                instructorRepository.Edit(instructor);
                instructorRepository.Commit();

                return Ok(instructor);
            }

            return BadRequest(instructor);
        }

        [HttpDelete]
        public IActionResult Delete(int instructorId)
        {
            var instructor = instructorRepository.GetOne(expression: e => e.InstructorId == instructorId);
            if (instructor != null)
            {
                instructorRepository.Delete(instructor);
                instructorRepository.Commit();
                return Ok(instructor);
            }
            else
                return NotFound();
        }
    }
}
