using Microsoft.AspNetCore.Mvc;
using firstpr.Models;

namespace firstpr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherService _service;
        public TeacherController(TeacherService service) => _service = service;
        
        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());
        
        [HttpGet("{id}")]
        public IActionResult GetById(string id) => _service.GetById(id) is var c && c != null ? Ok(c) : NotFound();
        
        [HttpPost]
        public IActionResult Add(Teacher teacher) { _service.Add(teacher); return Ok(teacher); }
        
        [HttpPut]
        public IActionResult Update(Teacher updatedTeacher)
        {
            _service.Update(updatedTeacher);
            return Ok(updatedTeacher);
        } 
        
        [HttpDelete("{id}")]
        public IActionResult DeleteById(string id)
        {
            _service.DeleteById(id);
            return NoContent();
        }
    }
}
