
using firstpr.Models;
using firstpr.Helpers;

namespace firstpr.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public bool Add(Student student)
        {
            if (student == null)
                return false;

            if (student.Id <= 0)
                return false;

            if (_repository.GetById(student.Id) != null)
                return false;

            if (!string.IsNullOrWhiteSpace(student.Email))
                ValidationHelper.ValidateEmail(student.Email);

            if (!string.IsNullOrWhiteSpace(student.PhoneNumber))
                ValidationHelper.ValidatePhoneNumber(student.PhoneNumber);

            if (student.DateOfBirth > DateOnly.FromDateTime(DateTime.Today))
                throw new ArgumentException("Date of birth cannot be in the future.");

            _repository.Add(student);
            return true;
        }

        public Student? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<Student> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(Student student)
        {
            if (student == null || student.Id <= 0)
                return;

            if (_repository.GetById(student.Id) != null)
            {
                _repository.Update(student);
            }
        }

        public void DeleteById(int id)
        {
            if (id <= 0)
                return;

            if (_repository.GetById(id) != null)
            {
                _repository.Delete(id);
            }
        }
    }
}




























