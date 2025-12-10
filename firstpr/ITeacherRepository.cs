using System.Collections.Generic;
using firstpr.Models;
using firstpr;

namespace firstpr
{
    public interface ITeacherRepository
    {
        List<Teacher> GetAll();
        Teacher GetById(string id);  
        void Add(Teacher teacher);
        void Update(Teacher teacher);
        void Delete(string id);
    }
}