using System.Text.Json.Serialization;

namespace firstpr.Models
{
    public class Student
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        [JsonIgnore]
        public List<Course> Courses { get; set; } = new();

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, DOB: {DateOfBirth}, Email: {Email}, Phone: {PhoneNumber}";
        }
    }
}