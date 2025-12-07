using firstpr;
public class StudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public void Add(Student student) => _repository.Add(student);
    public Student GetById(string id) => _repository.GetById(id);
    public List<Student> GetAll() => _repository.GetAll();
    public void Update(Student student) => _repository.Update(student);
    public void DeleteById(string id) => _repository.Delete(id);
}





































