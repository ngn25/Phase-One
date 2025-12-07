using firstpr;
public class CourseService
{
    private readonly ICourseRepository _repository;
    private readonly StudentService _studentService;
    private readonly TeacherService _teacherService;

    public CourseService(StudentService studentService, TeacherService teacherService, ICourseRepository repository = null)
    {
        _studentService = studentService;
        _teacherService = teacherService;
        _repository = repository ?? new CourseRepository();
    }

    public void Add(Course course)
    {
        if (string.IsNullOrEmpty(course.Id) || string.IsNullOrEmpty(course.Name) || string.IsNullOrEmpty(course.TeacherId))
            return;

        if (_teacherService.GetById(course.TeacherId) == null)
            return;

        if (course.StudentIds != null)
        {
            foreach (var sid in course.StudentIds)
            {
                if (_studentService.GetById(sid) == null)
                    return;
            }
        }

        _repository.Add(course);
    }

    public Course GetById(string id) => _repository.GetById(id);

    public List<Course> GetAll() => _repository.GetAll();

    public void Update(Course course) => _repository.Update(course);

    public void DeletById(string id) => _repository.Delete(id);  // اسم متد تو کد اصلیت DeletById هست
}