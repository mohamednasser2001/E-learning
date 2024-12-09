using AutoMapper;
using Data.IRepository;
using Data.Repository;
using Learnify.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Learnify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository courseRepository;
        private readonly IMapper mapper;

        public CourseController(ICourseRepository courseRepository,IMapper mapper)
        {
            this.courseRepository = courseRepository;
            this.mapper = mapper;
        }

        [HttpGet("get all")]
        public IActionResult Index()
        {
            var courses = courseRepository.GetAll();
            var result = mapper.Map<IEnumerable<AssignmentDTO>>(courses);
            return Ok(result);
        }

        [HttpGet("Details")]
        public IActionResult Details(int courseId)
        {
            var course = courseRepository.GetOne(expression: e => e.CourseId == courseId);
            if (course != null)
            {
                var result = mapper.Map<CourseDTO>(course);
                return Ok(course);
            }
            else
                return NotFound();
        }

        [HttpPost("create")]
        public IActionResult Create(CourseDTO courseDTO)
        {
            if (ModelState.IsValid)
            {
                var course = mapper.Map<Course>(courseDTO);
                courseRepository.Create(course);
                courseRepository.Commit();

                return Created();
            }

            return BadRequest(courseDTO);
        }

        [HttpPut("edit")]
        public IActionResult Edit(int coursId ,CourseDTO courseDTO)
        {
            var course = courseRepository.GetOne(expression: e => e.CourseId == coursId);
            if (course == null) return NotFound();

            if (ModelState.IsValid)
            {
                mapper.Map(courseDTO, course);
                courseRepository.Edit(course);
                courseRepository.Commit();

                return Ok(course);
            }

            return BadRequest(courseDTO);
        }

        [HttpDelete]
        public IActionResult Delete(int courseId)
        {
            var course = courseRepository.GetOne(expression: e => e.CourseId == courseId);
            if (course != null)
            {
                courseRepository.Delete(course);
                courseRepository.Commit();
                return Ok(course);
            }
            else
                return NotFound();
        }
    }
}
