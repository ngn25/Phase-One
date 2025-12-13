using firstpr.Models;
using firstpr;

namespace firstpr
{
public class CourseService
{
    private readonly ICourseRepository _repository;
    private readonly StudentService _studentService;
    private readonly TeacherService _teacherService;

    public CourseService(StudentService studentService, TeacherService teacherService, ICourseRepository repository)
    {
        _studentService = studentService;
        _teacherService = teacherService;
        _repository = repository;
    }

        public void Add(Course course)
        {
            if (_repository.GetById(course.Id) != null)
            {
                Console.WriteLine($"Error: Course with id '{course.Id}' already exists!");
                return;
            }

            var teacher = _teacherService.GetById(course.TeacherId);
            if (teacher == null)
            {
                Console.WriteLine($"Error: Teacher with id '{course.TeacherId}' not found!");
                return;
            }
            course.Teacher = teacher;

            List<Student> students = new List<Student>();
            // چک کردن وجود همه دانشجویان
            foreach (var student in course.Students)
            {
                var studentInDb = _studentService.GetById(student.Id);
                if (studentInDb == null)
                {
                    Console.WriteLine($"Error: Student with id '{student.Id}' not found!");
                    return;
                }
                students.Add(studentInDb);
            }
            course.Students = students;

            _repository.Add(course);
            Console.WriteLine("Course added successfully.");
        }

        public void Update(Course course)
        {
            if (_repository.GetById(course.Id) == null)
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

            List<Student> students = new List<Student>();
            foreach (var student in course.Students)
            {
                var studentInDb = _studentService.GetById(student.Id);
                if (studentInDb == null)
                {
                    Console.WriteLine($"Error: Student with id '{student.Id}' not found!");
                    return;
                }
                students.Add(studentInDb);
            }
            course.Students = students;

            _repository.Update(course);
            Console.WriteLine("Course updated successfully.");
        }

        public Course? GetById(string id) => _repository.GetById(id);

        public List<Course> GetAll() => _repository.GetAll();

        public void DeleteById(string id)
        {
            if (_repository.GetById(id) == null)
            {
                Console.WriteLine($"Course with id '{id}' not found!");
                return;
            }
            _repository.Delete(id);
            Console.WriteLine("Course removed successfully.");
        }
    }
}