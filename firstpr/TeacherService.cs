using System.Collections.Generic;
using firstpr;

public class TeacherService
{
    private readonly ITeacherRepository _repository;

    public TeacherService(ITeacherRepository repository)
    {
        _repository = repository;
    }

    public TeacherService()
    {
        _repository = new TeacherRepository();
    }

    public void Add(Teacher teacher) => _repository.Add(teacher);

    public Teacher GetById(string Id) => _repository.GetById(Id);

    public List<Teacher> GetAll() => _repository.GetAll();

    public void Update(Teacher teacher) => _repository.Update(teacher);

    public void DeleteById(string Id) => _repository.Delete(Id);
}