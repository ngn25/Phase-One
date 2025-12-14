
namespace firstpr.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

  
        public List<Course> Courses { get; set; } = new();

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, DOB: {DateOfBirth}, Email: {Email}, Phone: {PhoneNumber}";
        }
    }
}