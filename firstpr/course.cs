namespace firstpr
{
    public class Course
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string TeacherId { get; set; } = "";
        public List<string> StudentIds { get; set; } = new();

        public override string ToString()
        {
            return $"{Id} | {Name} | Teacher: {TeacherId} | Students: {StudentIds.Count} [{string.Join(", ", StudentIds)}]";
        }
    }
}