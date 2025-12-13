using System.Collections.Generic;
using firstpr.Models;
using firstpr;



    public class TeacherService
{
    private readonly ITeacherRepository _repository;

    public TeacherService(ITeacherRepository repository)
    {
        _repository = repository;
    }

    public void Add(Teacher teacher)
    {
        if (teacher == null || string.IsNullOrEmpty(teacher.Id)) return;

        if (_repository.GetById(teacher.Id) == null)
        {
            _repository.Add(teacher);
        }
    }

    public Teacher GetById(string Id)
    {
        return _repository.GetById(Id);
    }

    public List<Teacher> GetAll()
    {
        return _repository.GetAll();
    }

    public void Update(Teacher teacher)
    {
        if (teacher == null || string.IsNullOrEmpty(teacher.Id)) return;

        if (_repository.GetById(teacher.Id) != null)
        {
            _repository.Update(teacher);
        }
    }

    public void DeleteById(string Id)
    {
        if (string.IsNullOrEmpty(Id)) return;

        if (_repository.GetById(Id) != null)
        {
            _repository.Delete(Id);
        }
    }
}