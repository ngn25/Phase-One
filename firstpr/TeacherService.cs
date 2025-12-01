public class TeacherService
{
    private Dictionary<string, Teacher> Data = new Dictionary<string, Teacher>();

    public void Add(Teacher teacher)
    {
        if (!Data.ContainsKey(teacher.Id))
        {
            Data.Add(teacher.Id, teacher);
        }
    }

    public Teacher GetById(string Id)
    {
        if (Data.ContainsKey(Id))
        {
            return Data[Id];

        }
        return null;
    }
    public List<Teacher> GetAll()
    {
        return Data.Values.ToList();
    }

    public void Update(Teacher teacher)
    {
        if (Data.ContainsKey(teacher.Id))
        {
            Data[teacher.Id] = teacher;

        }
    }
    public void DeleteById(string Id)
    {
        Data.Remove(Id);
    }
} 