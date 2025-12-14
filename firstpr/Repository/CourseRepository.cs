using Microsoft.EntityFrameworkCore;
using firstpr.Models;

namespace firstpr.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SchoolDbContext _context;

        public CourseRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public List<Course> GetAll()
        {
            return _context.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .ToList();
        }

        public Course? GetById(int id)
        {
            if (id <= 0)
                return null;

            return _context.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .FirstOrDefault(c => c.Id == id);
        }

        public void Add(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public void Update(Course course)
        {
            var existing = _context.Courses
                .Include(c => c.Students)
                .FirstOrDefault(c => c.Id == course.Id);

            if (existing == null)
                return;

            // Update scalar properties
            _context.Entry(existing).CurrentValues.SetValues(course);

            // Update students (many-to-many)
            existing.Students.Clear();
            foreach (var student in course.Students)
            {
                var s = _context.Students.Find(student.Id);
                if (s != null)
                    existing.Students.Add(s);
            }

            // Update teacher (many-to-one)
            if (course.TeacherId > 0)
            {
                var teacher = _context.Teachers.Find(course.TeacherId);
                if (teacher != null)
                    existing.Teacher = teacher;
            }

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            if (id <= 0)
                return;

            var course = _context.Courses.Find(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
        }
    }
}
