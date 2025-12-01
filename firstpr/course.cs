public class Course
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string TeacherId { get; set; }

    public List<string> StudentIds { get; set; }
    public Course(string Id, string Name, string TeacherId, List<string> StudentIds)
    {
        this.Id = Id;
        this.Name = Name;
        this.TeacherId = TeacherId;
        this.StudentIds = StudentIds;
    }
    public override string ToString()
    {
        return "Id: " + Id + ", Name: " + Name + ",TeacherId :" + TeacherId + ",StudentIds:" + StudentIds;
    }


}