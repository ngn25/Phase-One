namespace firstpr.Models
{
    public class Course
    {
        public int Id { get; set; }             
        public string Name { get; set; } = string.Empty;

        public int TeacherId { get; set; }        

        public Teacher? Teacher { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();

        public override string ToString()
        {
            return $"{Id} | {Name} | Teacher: {Teacher?.Name ?? "None"} | Students: {Students.Count}";
        }
    }
}
