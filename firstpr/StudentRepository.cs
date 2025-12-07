using Microsoft.Data.SqlClient;

namespace firstpr  
{
    public class StudentRepository : IStudentRepository
    {
        public List<Student> GetAll()
        {
            var list = new List<Student>();
            using var conn = DatabaseConnection.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SELECT Id, Name, DateOfBirth, Email, PhoneNumber FROM Students", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Student
                {
                    Id = reader.GetString(0),
                    Name = reader.GetString(1),
                    DateOfBirth = DateOnly.FromDateTime(reader.GetDateTime(2)),
                    Email = reader.GetString(3),
                    PhoneNumber = reader.GetString(4)
                });
            }
            return list;
        }

        public Student GetById(string id)
        {
            using var conn = DatabaseConnection.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SELECT Id, Name, DateOfBirth, Email, PhoneNumber FROM Students WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? new Student
            {
                Id = reader.GetString(0),
                Name = reader.GetString(1),
                DateOfBirth = DateOnly.FromDateTime(reader.GetDateTime(2)),
                Email = reader.GetString(3),
                PhoneNumber = reader.GetString(4)
            } : null;
        }

        public void Add(Student student)
        {
            using var conn = DatabaseConnection.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(@"INSERT INTO Students (Id, Name, DateOfBirth, Email, PhoneNumber) 
                                            VALUES (@Id, @Name, @DOB, @Email, @Phone)", conn);
            cmd.Parameters.AddWithValue("@Id", student.Id);
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@DOB", student.DateOfBirth.ToDateTime(TimeOnly.MinValue));
            cmd.Parameters.AddWithValue("@Email", student.Email ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Phone", student.PhoneNumber ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Update(Student student)
        {
            using var conn = DatabaseConnection.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(@"UPDATE Students SET Name = @Name, DateOfBirth = @DOB, 
                                            Email = @Email, PhoneNumber = @Phone WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", student.Id);
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@DOB", student.DateOfBirth.ToDateTime(TimeOnly.MinValue));
            cmd.Parameters.AddWithValue("@Email", student.Email ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Phone", student.PhoneNumber ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Delete(string id)
        {
            using var conn = DatabaseConnection.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM Students WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}