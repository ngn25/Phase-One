using System.Text.Json.Serialization;

namespace firstpr.Models
{
    public class Teacher
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        [JsonIgnore]
        public List<Course> CoursesTaught { get; set; } = new();

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Email: {Email}, Phone: {PhoneNumber}";
        }
    }
}