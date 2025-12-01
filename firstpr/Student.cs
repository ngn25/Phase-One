
public class Student
{
    public string Id { get; set; }

    public string Name { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }



    public Student(string Id, string Name, DateOnly DateOfBirth, string Email, string PhoneNumber)
    {
        this.Id = Id;
        this.Name = Name;
        this.DateOfBirth = DateOfBirth;
        this.Email = Email;
        this.PhoneNumber = PhoneNumber;
    }

    public override string ToString()
    {
        return "Id: " + Id + ", Name: " + Name + ", DateOfBirth:" + DateOfBirth + ",Email:" + Email + ",PhonNumber:" + PhoneNumber;
    }
}