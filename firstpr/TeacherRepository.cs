using Microsoft.EntityFrameworkCore;
using firstpr.Models;

namespace firstpr
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SchoolDbContext _context;

        public TeacherRepository(SchoolDbContext context) => _context = context;

        public List<Teacher> GetAll() => _context.Teachers.ToList();

        public Teacher? GetById(string id) => _context.Teachers.Find(id);

        public void Add(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
        }

        public void Update(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                _context.SaveChanges();
            }
        }
    }
}