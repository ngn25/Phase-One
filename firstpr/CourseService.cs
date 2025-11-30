public class CourseService
{
    private Dictionary<string, Course > Data=new Dictionary<string, Course >();
    private StudentService StudentService;
    private TeacherService TeacherService;
    public CourseService(StudentService StudentService ,TeacherService TeacherService)
    {
        this.StudentService = StudentService;

         this.TeacherService=TeacherService;

    }

    public void Add(Course course)
    {
        if (!Data.ContainsKey(course.Id))
        {
             Teacher teacher = TeacherService.GetById(course.TeacherId);
            if (teacher != null)
            {
                if (DoStudentExist(course.studeniIds))
                {
                    Data.Add(course.Id,course);   
                }
            
            }                
        }
    }

    public Course GetById(string Id) 
    {
        if (Data.ContainsKey(Id))
        {
            return Data[Id];
    
        }
        return null;
    }
     public List<Course> GetAll()
    {
        return Data.Values.ToList();
    }

    public void Update(Course course)
    {
        if (Data.ContainsKey(course.Id))
        {
        Teacher teacher=TeacherService.GetById(course.TeacherId);
             if (teacher != null)
            {
                  Data[course.Id] = course;
            }
        }
    }
    public void DeletById(string Id)
    {
        Data.Remove(Id);
    }

    private bool DoStudentExist(List<string> studeniIds)
    {
        return true;
    }


    
}
