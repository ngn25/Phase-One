using Microsoft.AspNetCore.Mvc;
using firstpr.Models;

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
        public IActionResult GetById(string id) => _service.GetById(id) is var c && c != null ? Ok(c) : NotFound();
        
        [HttpPost]
        public IActionResult Add(Course course) { _service.Add(course); return Ok(course); }
        
        [HttpPut]
        public IActionResult Update(Course updatedCourse)
        {
            _service.Update(updatedCourse);
            return Ok(updatedCourse);
        } 
        
        [HttpDelete("{id}")]
        public IActionResult DeleteById(string id)
        {
            _service.DeleteById(id);
            return NoContent();
        }
    }
}
