using System.Collections.Generic;

namespace firstpr
{
    public interface ITeacherRepository
    {
        List<Teacher> GetAll();
        Teacher GetById(string id);   // ← اینجا باید Teacher باشه، نه Student!
        void Add(Teacher teacher);
        void Update(Teacher teacher);
        void Delete(string id);
    }
}