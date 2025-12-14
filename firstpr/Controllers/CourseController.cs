using Microsoft.AspNetCore.Mvc;
using firstpr.Models;
using firstpr.Services;

namespace firstpr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _service;
        public CourseController(CourseService service) => _service = service;

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var course = _service.GetById(id);
            return course != null ? Ok(course) : NotFound();
        }

        [HttpPost]
        public IActionResult Add(Course course, [FromQuery] List<int> studentIds)
        {
            if (course == null)
                return BadRequest("Course is null.");

            _service.Add(course, studentIds);
            return Ok(course);
        }

        [HttpPut]
        public IActionResult Update(Course updatedCourse, [FromQuery] List<int> studentIds)
        {
            if (updatedCourse == null)
                return BadRequest("Course is null.");

            _service.Update(updatedCourse, studentIds);
            return Ok(updatedCourse);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            _service.DeleteById(id);
            return NoContent();
        }
    }
}

