using System.Collections.Generic;

namespace firstpr
{
    public interface ICourseRepository
    {
        List<Course> GetAll();
        Course GetById(string id);
        void Add(Course course);
        void Update(Course course);
        void Delete(string id);
    }
}