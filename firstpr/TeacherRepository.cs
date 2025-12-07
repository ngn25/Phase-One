using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace firstpr   
{
    public class TeacherRepository : ITeacherRepository
    {
        public List<Teacher> GetAll()
        {
            var list = new List<Teacher>();
            using var conn = DatabaseConnection.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SELECT Id, Name, DateOfBirth, Email, PhoneNumber FROM Teachers", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Teacher
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

        public Teacher GetById(string id)   
        {
            using var conn = DatabaseConnection.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SELECT Id, Name, DateOfBirth, Email, PhoneNumber FROM Teachers WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? new Teacher
            {
                Id = reader.GetString(0),
                Name = reader.GetString(1),
                DateOfBirth = DateOnly.FromDateTime(reader.GetDateTime(2)),
                Email = reader.GetString(3),
                PhoneNumber = reader.GetString(4)
            } : null;
        }

        public void Add(Teacher teacher)
        {
            using var conn = DatabaseConnection.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(@"INSERT INTO Teachers (Id, Name, DateOfBirth, Email, PhoneNumber) 
                                            VALUES (@Id, @Name, @DOB, @Email, @Phone)", conn);
            cmd.Parameters.AddWithValue("@Id", teacher.Id);
            cmd.Parameters.AddWithValue("@Name", teacher.Name);
            cmd.Parameters.AddWithValue("@DOB", teacher.DateOfBirth.ToDateTime(TimeOnly.MinValue));
            cmd.Parameters.AddWithValue("@Email", teacher.Email ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Phone", teacher.PhoneNumber ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Update(Teacher teacher)
        {
            using var conn = DatabaseConnection.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(@"UPDATE Teachers SET Name = @Name, DateOfBirth = @DOB,
                                            Email = @Email, PhoneNumber = @Phone WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", teacher.Id);
            cmd.Parameters.AddWithValue("@Name", teacher.Name);
            cmd.Parameters.AddWithValue("@DOB", teacher.DateOfBirth.ToDateTime(TimeOnly.MinValue));
            cmd.Parameters.AddWithValue("@Email", teacher.Email ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Phone", teacher.PhoneNumber ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Delete(string id)
        {
            using var conn = DatabaseConnection.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM Teachers WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}