using Microsoft.AspNetCore.Mvc;
using firstpr.Models;

namespace firstpr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _service;
        public StudentController(StudentService service) => _service = service;
        
        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());
        
        [HttpGet("{id}")]
        public IActionResult GetById(string id) => _service.GetById(id) is var c && c != null ? Ok(c) : NotFound();
        
        [HttpPost]
        public IActionResult Add(Student student) { _service.Add(student); return Ok(student); }
        
        [HttpPut]
        public IActionResult Update(Student updatedStudent)
        {
            _service.Update(updatedStudent);
            return Ok(updatedStudent);
        } 
        
        [HttpDelete("{id}")]
        public IActionResult DeleteById(string id)
        {
            _service.DeleteById(id);
            return NoContent();
        }
    }
}
