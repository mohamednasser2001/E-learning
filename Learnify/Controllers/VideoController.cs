using Data.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Learnify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoRepository videoRepository;

        public VideoController(IVideoRepository videoRepository)
        {
            this.videoRepository = videoRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = videoRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("Details")]
        public IActionResult Details(int videoId)
        {
            var video = videoRepository.GetOne(expression: e => e.VideoId == videoId);
            if (video != null)
                return Ok(video);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Create(Video video)
        {
            if (ModelState.IsValid)
            {
                videoRepository.Create(video);
                videoRepository.Commit();

                return Created();
            }

            return BadRequest(video);
        }

        [HttpPut]
        public IActionResult Edit(Video video)
        {
            if (ModelState.IsValid)
            {
                videoRepository.Edit(video);
                videoRepository.Commit();

                return Ok(video);
            }

            return BadRequest(video);
        }

        [HttpDelete]
        public IActionResult Delete(int videoId)
        {
            var video = videoRepository.GetOne(expression: e => e.VideoId == videoId);
            if (video != null)
            {
                videoRepository.Delete(video);
                videoRepository.Commit();
                return Ok(video);
            }
            else
                return NotFound();
        }
    }
}
