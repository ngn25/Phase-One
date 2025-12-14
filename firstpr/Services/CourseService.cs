using firstpr.Models;

namespace firstpr.Services
{
    public class CourseService
    {
        private readonly ICourseRepository _repository;
        private readonly StudentService _studentService;
        private readonly TeacherService _teacherService;

        public CourseService(
            StudentService studentService,
            TeacherService teacherService,
            ICourseRepository repository)
        {
            _studentService = studentService;
            _teacherService = teacherService;
            _repository = repository;
        }

        public void Add(Course course, List<int> studentIds)
        {
            if (course == null)
                return;

            if (course.Id != 0)
            {
                Console.WriteLine("Error: New course must not have an Id.");
                return;
            }

            var teacher = _teacherService.GetById(course.TeacherId);
            if (teacher == null)
            {
                Console.WriteLine($"Error: Teacher with id '{course.TeacherId}' not found!");
                return;
            }
            course.Teacher = teacher;

            // Clear existing CourseStudents and add new
            course.CourseStudents.Clear();
            foreach (var studentId in studentIds)
            {
                var student = _studentService.GetById(studentId);
                if (student == null)
                {
                    Console.WriteLine($"Error: Student with id '{studentId}' not found!");
                    return;
                }

                course.CourseStudents.Add(new CourseStudent
                {
                    Course = course,
                    Student = student
                });
            }

            _repository.Add(course);
            Console.WriteLine("Course added successfully.");
        }

        public void Update(Course course, List<int> studentIds)
        {
            if (course == null || course.Id <= 0)
                return;

            var existingCourse = _repository.GetById(course.Id);
            if (existingCourse == null)
            {
                Console.WriteLine($"Error: Course with id '{course.Id}' not found!");
                return;
            }

            var teacher = _teacherService.GetById(course.TeacherId);
            if (teacher == null)
            {
                Console.WriteLine($"Error: Teacher with id '{course.TeacherId}' not found!");
                return;
            }
            course.Teacher = teacher;

            // Clear old students and add new
            course.CourseStudents.Clear();
            foreach (var studentId in studentIds)
            {
                var student = _studentService.GetById(studentId);
                if (student == null)
                {
                    Console.WriteLine($"Error: Student with id '{studentId}' not found!");
                    return;
                }

                course.CourseStudents.Add(new CourseStudent
                {
                    Course = course,
                    Student = student
                });
            }

            _repository.Update(course);
            Console.WriteLine("Course updated successfully.");
        }

        public Course? GetById(int id)
        {
            if (id <= 0)
                return null;

            return _repository.GetById(id);
        }

        public List<Course> GetAll()
        {
            return _repository.GetAll();
        }

        public void DeleteById(int id)
        {
            if (id <= 0)
                return;

            var course = _repository.GetById(id);
            if (course == null)
            {
                Console.WriteLine($"Course with id '{id}' not found!");
                return;
            }

            _repository.Delete(id);
            Console.WriteLine("Course removed successfully.");
        }
    }
}
