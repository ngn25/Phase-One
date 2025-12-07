using Microsoft.Data.SqlClient;

namespace firstpr
{
    public class CourseRepository : ICourseRepository
    {
        public List<Course> GetAll()
        {
            var list = new List<Course>();

            using var conn = DatabaseConnection.GetConnection();
            conn.Open();

            // 1. همه درس‌ها رو بخون
            using var cmd = new SqlCommand("SELECT Id, Name, TeacherId FROM Courses", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Course
                {
                    Id = reader.GetString(0),
                    Name = reader.GetString(1),
                    TeacherId = reader.GetString(2),
                    StudentIds = new List<string>()
                });
            }
            reader.Close(); // مهمه!

            // 2. دانشجوهای هر درس رو بخون
            using var cmd2 = new SqlCommand("SELECT CourseId, StudentId FROM CourseStudents", conn);
            using var reader2 = cmd2.ExecuteReader();

            while (reader2.Read())
            {
                string courseId = reader2.GetString(0);
                string studentId = reader2.GetString(1);

                var course = list.Find(c => c.Id == courseId);
                if (course != null)
                    course.StudentIds.Add(studentId);
            }

            return list;
        }

        public Course? GetById(string id)
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

            // 1. درس رو اضافه کن
            using var cmd = new SqlCommand(@"INSERT INTO Courses (Id, Name, TeacherId) 
                                            VALUES (@Id, @Name, @TeacherId)", conn);
            cmd.Parameters.AddWithValue("@Id", course.Id);
            cmd.Parameters.AddWithValue("@Name", course.Name);
            cmd.Parameters.AddWithValue("@TeacherId", course.TeacherId ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();

            // 2. دانشجوها رو ثبت‌نام کن
            foreach (var sid in course.StudentIds)
            {
                using var cmd2 = new SqlCommand(@"INSERT INTO CourseStudents (CourseId, StudentId) 
                                                VALUES (@CourseId, @StudentId)", conn);
                cmd2.Parameters.AddWithValue("@CourseId", course.Id);
                cmd2.Parameters.AddWithValue("@StudentId", sid);
                cmd2.ExecuteNonQuery();
            }
        }

        public void Update(Course course)
        {
            using var conn = DatabaseConnection.GetConnection();
            conn.Open();

            // 1. درس رو آپدیت کن
            using var cmd = new SqlCommand(@"UPDATE Courses SET Name = @Name, TeacherId = @TeacherId WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", course.Id);
            cmd.Parameters.AddWithValue("@Name", course.Name);
            cmd.Parameters.AddWithValue("@TeacherId", course.TeacherId ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();

            // 2. ثبت‌نام‌های قبلی رو پاک کن
            using var cmdDel = new SqlCommand("DELETE FROM CourseStudents WHERE CourseId = @Id", conn);
            cmdDel.Parameters.AddWithValue("@Id", course.Id);
            cmdDel.ExecuteNonQuery();

            // 3. دانشجوهای جدید رو اضافه کن
            foreach (var sid in course.StudentIds)
            {
                using var cmd2 = new SqlCommand(@"INSERT INTO CourseStudents (CourseId, StudentId) 
                                                VALUES (@CourseId, @StudentId)", conn);
                cmd2.Parameters.AddWithValue("@CourseId", course.Id);
                cmd2.Parameters.AddWithValue("@StudentId", sid);
                cmd2.ExecuteNonQuery();
            }
        }

        public void Delete(string id)
        {
            using var conn = DatabaseConnection.GetConnection();
            conn.Open();

            // CourseStudents به خاطر ON DELETE CASCADE خودکار پاک می‌شه
            using var cmd = new SqlCommand("DELETE FROM Courses WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}