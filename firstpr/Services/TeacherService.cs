
using firstpr.Models;
using firstpr.Helpers;
namespace firstpr.Services
{
    public class TeacherService
    {
        private readonly ITeacherRepository _repository;

        public TeacherService(ITeacherRepository repository)
        {
            _repository = repository;
        }

        public void Add(Teacher teacher)
        {
            if (teacher == null)
                return;

            if (!string.IsNullOrWhiteSpace(teacher.Email))
                ValidationHelper.ValidateEmail(teacher.Email);

            if (!string.IsNullOrWhiteSpace(teacher.PhoneNumber))
                ValidationHelper.ValidatePhoneNumber(teacher.PhoneNumber);

            if (teacher.Id != 0)
                return;

            _repository.Add(teacher);
        }

        public Teacher? GetById(int id)
        {
            if (id <= 0)
                return null;

            return _repository.GetById(id);
        }

        public List<Teacher> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(Teacher teacher)
        {
            if (teacher == null || teacher.Id <= 0)
                return;

            var existing = _repository.GetById(teacher.Id);
            if (existing == null)
                return;

            // Validation FIRST
            if (!string.IsNullOrWhiteSpace(teacher.Email))
                ValidationHelper.ValidateEmail(teacher.Email);

            if (!string.IsNullOrWhiteSpace(teacher.PhoneNumber))
                ValidationHelper.ValidatePhoneNumber(teacher.PhoneNumber);

            _repository.Update(teacher);
        }

        public void DeleteById(int id)
        {
            if (id <= 0)
                return;

            var teacher = _repository.GetById(id);
            if (teacher != null)
            {
                _repository.Delete(id);
            }
        }
    }
}
