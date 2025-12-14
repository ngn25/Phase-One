using firstpr.Models;

namespace firstpr
{
    public interface ITeacherRepository
    {
        List<Teacher> GetAll();
        Teacher GetById(int id);  
        void Add(Teacher teacher);
        void Update(Teacher teacher);
        void Delete(int id);
    }
}