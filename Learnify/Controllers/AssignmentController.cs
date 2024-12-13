using AutoMapper;
using Data.IRepository;
using Learnify.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Learnify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentRepository assignmentRepository;
        private readonly IMapper mapper;

        public AssignmentController(IAssignmentRepository assignmentRepository, IMapper mapper)
        {
            this.assignmentRepository = assignmentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var assignments = assignmentRepository.GetAll();
            var result = mapper.Map<IEnumerable<AssignmentDTO>>(assignments);
            return Ok(result);
        }

        [HttpGet("Details")]
        public IActionResult Details(int assignmentId)
        {
            var assignment = assignmentRepository.GetOne(expression: e => e.AssignmentId == assignmentId);
            if (assignment != null)
            {
                var result = mapper.Map<AssignmentDTO>(assignment);
                return Ok(result);
            }
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Create(AssignmentDTO assignmentDTO)
        {
            if (ModelState.IsValid)
            {
                var assignment = mapper.Map<Assignment>(assignmentDTO);
                assignmentRepository.Add(assignment);
                assignmentRepository.Commit();

                return Created();
            }

            return BadRequest(assignmentDTO);
        }

        [HttpPut]
        public IActionResult Edit(int assignmentId, AssignmentDTO assignmentDTO)
        {
            var assignment = assignmentRepository.GetOne(expression: e => e.AssignmentId == assignmentId);
            if (assignment == null) return NotFound();

            if (ModelState.IsValid)
            {
                mapper.Map(assignmentDTO, assignment); // Update the existing entity
                assignmentRepository.Edit(assignment);
                assignmentRepository.Commit();

                return Ok(assignment);
            }

            return BadRequest(assignmentDTO);
        }

        [HttpDelete]
        public IActionResult Delete(int assignmentId)
        {
            var assignment = assignmentRepository.GetOne(expression: e => e.AssignmentId == assignmentId);
            if (assignment != null)
            {
                assignmentRepository.Delete(assignment);
                assignmentRepository.Commit();
                return Ok(assignment);
            }
            else
                return NotFound();
        }
    }
}
