using System.Collections.Generic;
using firstpr;

public class CourseService
{
    private readonly StudentService _studentService;
    private readonly TeacherService _teacherService;
    private readonly ICourseRepository _repository;

    public CourseService(StudentService studentService,
                         TeacherService teacherService,
                         ICourseRepository repository)
    {
        _studentService = studentService;
        _teacherService = teacherService;
        _repository = repository;
    }

    public void Add(Course course)
    {
        if (_repository.GetById(course.Id) != null)
            return;

        Teacher teacher = _teacherService.GetById(course.TeacherId);
        if (teacher == null)
            return;

        if (!DoStudentsExist(course.StudentIds))
            return;

        _repository.Add(course);
    }

    public Course GetById(string Id)
    {
        return _repository.GetById(Id);
    }

    public List<Course> GetAll()
    {
        return _repository.GetAll();
    }

    public void Update(Course course)
    {
        if (_repository.GetById(course.Id) == null)
            return;

        Teacher teacher = _teacherService.GetById(course.TeacherId);
        if (teacher == null)
            return;

        if (!DoStudentsExist(course.StudentIds))
            return;

        _repository.Update(course);
    }

    public void DeletById(string Id)
    {
        if (_repository.GetById(Id) == null)
            return;

        _repository.Delete(Id);
    }

    private bool DoStudentsExist(List<string> studentIds)
    {
        if (studentIds == null) return true;

        foreach (var id in studentIds)
        {
            Student student = _studentService.GetById(id);
            if (student == null)
                return false;
        }
        return true;
    }
}
