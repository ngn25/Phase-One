using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace firstpr
{
    public class CourseRepository : ICourseRepository
    {
        public List<Course> GetAll()
        {
            var courses = new List<Course>();

            using var conn = DatabaseConnection.GetConnection();
            conn.Open();

            // اول همه درس‌ها رو از جدول Courses می‌خونیم
            using var cmd = new SqlCommand("SELECT Id, Name, TeacherId FROM Courses", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                courses.Add(new Course
                {
                    Id = reader.GetString(0),
                    Name = reader.GetString(1),
                    TeacherId = reader.GetString(2),
                    StudentIds = new List<string>()
                });
            }
            reader.Close();

            // حالا برای هر درس، دانشجوهاش رو از جدول CourseStudents می‌گیریم
            foreach (var course in courses)
            {
                using var cmd2 = new SqlCommand("SELECT StudentId FROM CourseStudents WHERE CourseId = @CourseId", conn);
                cmd2.Parameters.AddWithValue("@CourseId", course.Id);
                using var reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    course.StudentIds.Add(reader2.GetString(0));
                }
            }

            return courses;
        }

        public Course GetById(string id)
        {
            using var conn = DatabaseConnection.GetConnection();
            conn.Open();

            using var cmd = new SqlCommand("SELECT Id, Name, TeacherId FROM Courses WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();

            if (!reader.Read()) return null;

            var course = new Course
            {
                Id = reader.GetString(0),
                Name = reader.GetString(1),
                TeacherId = reader.GetString(2),
                StudentIds = new List<string>()
            };
            reader.Close();

            // دانشجوهای این درس رو بگیر
            using var cmd2 = new SqlCommand("SELECT StudentId FROM CourseStudents WHERE CourseId = @Id", conn);
            cmd2.Parameters.AddWithValue("@Id", id);
            using var reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                course.StudentIds.Add(reader2.GetString(0));
            }

            return course;
        }

        public void Add(Course course)
        {
            using var conn = DatabaseConnection.GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                // ۱. درس رو به جدول Courses اضافه کن
                using var cmd = new SqlCommand("INSERT INTO Courses (Id, Name, TeacherId) VALUES (@Id, @Name, @TeacherId)", conn, transaction);
                cmd.Parameters.AddWithValue("@Id", course.Id);
                cmd.Parameters.AddWithValue("@Name", course.Name);
                cmd.Parameters.AddWithValue("@TeacherId", course.TeacherId);
                cmd.ExecuteNonQuery();

                // ۲. دانشجوها رو به جدول CourseStudents اضافه کن
                foreach (var studentId in course.StudentIds)
                {
                    using var cmd2 = new SqlCommand("INSERT INTO CourseStudents (CourseId, StudentId) VALUES (@CourseId, @StudentId)", conn, transaction);
                    cmd2.Parameters.AddWithValue("@CourseId", course.Id);
                    cmd2.Parameters.AddWithValue("@StudentId", studentId);
                    cmd2.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(Course course)
        {
            using var conn = DatabaseConnection.GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                // ۱. درس اصلی رو آپدیت کن
                using var cmd = new SqlCommand("UPDATE Courses SET Name = @Name, TeacherId = @TeacherId WHERE Id = @Id", conn, transaction);
                cmd.Parameters.AddWithValue("@Id", course.Id);
                cmd.Parameters.AddWithValue("@Name", course.Name);
                cmd.Parameters.AddWithValue("@TeacherId", course.TeacherId);
                cmd.ExecuteNonQuery();

                // ۲. دانشجوهای قبلی رو پاک کن
                using var cmdDel = new SqlCommand("DELETE FROM CourseStudents WHERE CourseId = @Id", conn, transaction);
                cmdDel.Parameters.AddWithValue("@Id", course.Id);
                cmdDel.ExecuteNonQuery();

                // ۳. دانشجوهای جدید رو اضافه کن
                foreach (var studentId in course.StudentIds)
                {
                    using var cmdIns = new SqlCommand("INSERT INTO CourseStudents (CourseId, StudentId) VALUES (@CourseId, @StudentId)", conn, transaction);
                    cmdIns.Parameters.AddWithValue("@CourseId", course.Id);
                    cmdIns.Parameters.AddWithValue("@StudentId", studentId);
                    cmdIns.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(string id)
        {
            using var conn = DatabaseConnection.GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                // اول دانشجوها رو پاک کن
                using var cmd1 = new SqlCommand("DELETE FROM CourseStudents WHERE CourseId = @Id", conn, transaction);
                cmd1.Parameters.AddWithValue("@Id", id);
                cmd1.ExecuteNonQuery();

                // بعد خود درس رو پاک کن
                using var cmd2 = new SqlCommand("DELETE FROM Courses WHERE Id = @Id", conn, transaction);
                cmd2.Parameters.AddWithValue("@Id", id);
                cmd2.ExecuteNonQuery();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}