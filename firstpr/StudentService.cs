using System.Collections.Generic;
using firstpr.Models;
using firstpr;



public class StudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public bool Add(Student student) 
  
    {
        if (student == null || string.IsNullOrEmpty(student.Id))
            return false;

        if (_repository.GetById(student.Id) != null)  
            return false;

        _repository.Add(student);
        return true;
    }

    public Student GetById(string Id)
    {
        return _repository.GetById(Id);
    }

    public List<Student> GetAll()
    {
        return _repository.GetAll();
    }

    public void Update(Student student)
    {
        if (student == null || string.IsNullOrEmpty(student.Id)) return;

        if (_repository.GetById(student.Id) != null)
        {
            _repository.Update(student);
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
































