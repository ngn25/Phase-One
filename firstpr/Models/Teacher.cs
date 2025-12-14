namespace firstpr.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Courses taught by this teacher
        public List<Course> CoursesTaught { get; set; } = new();

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Email: {Email}, Phone: {PhoneNumber}, Courses: {CoursesTaught.Count}";
        }
    }
}
