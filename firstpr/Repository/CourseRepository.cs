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
                .Include(c => c.CourseStudents)
                    .ThenInclude(cs => cs.Student)
                .ToList();
        }

        public Course? GetById(int id)
        {
            if (id <= 0)
                return null;

            return _context.Courses
                .Include(c => c.Teacher)
                .Include(c => c.CourseStudents)
                    .ThenInclude(cs => cs.Student)
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
                .Include(c => c.CourseStudents)
                .FirstOrDefault(c => c.Id == course.Id);

            if (existing == null)
                return;

            _context.Entry(existing).CurrentValues.SetValues(course);

            // Clear old CourseStudents and add new
            existing.CourseStudents.Clear();
            foreach (var cs in course.CourseStudents)
            {
                var student = _context.Students.Find(cs.StudentId);
                if (student != null)
                {
                    existing.CourseStudents.Add(new CourseStudent
                    {
                        CourseId = course.Id,
                        StudentId = student.Id,
                        Student = student,
                        Course = existing
                    });
                }
            }

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
