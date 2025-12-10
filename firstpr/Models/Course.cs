namespace firstpr.Models
{
    public class Course
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        
        // این خط رو حتماً اضافه کن (یا اگر هست نگه دار)
        public string TeacherId { get; set; } = null!;

        public Teacher? Teacher { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();

        public override string ToString()
        {
            return $"{Id} | {Name} | Teacher: {Teacher?.Name ?? "None"} | Students: {Students.Count}";
        }
    }
}