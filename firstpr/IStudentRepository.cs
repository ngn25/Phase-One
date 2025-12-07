using System.Collections.Generic;

namespace firstpr   
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Student GetById(string id);
        void Add(Student student);
        void Update(Student student);
        void Delete(string id);
    }
}