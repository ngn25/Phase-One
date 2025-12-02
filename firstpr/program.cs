using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        StudentService studentService = new StudentService();
        TeacherService teacherService = new TeacherService();
        CourseService courseService = new CourseService(studentService, teacherService);

        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            string[] parts = input.Split(' ');

            if (parts.Length < 2)
            {
                Console.WriteLine("Invalid command.");
                continue;
            }

            string target = parts[0];
            string action = parts[1];

            // ========================= STUDENT ADD ================================
            if (target.Equals("student", StringComparison.OrdinalIgnoreCase) &&
                action.Equals("add", StringComparison.OrdinalIgnoreCase))
            {
                if (parts.Length < 7)
                {
                    Console.WriteLine("Not enough parameters for student add!");
                    continue;
                }

                // تاریخ باید DateOnly باشد
                if (!DateOnly.TryParse(parts[4], out DateOnly dob))
                {
                    Console.WriteLine("Invalid date format!");
                    continue;
                }

                Student student = new Student()
                {
                    Id = parts[2],
                    Name = parts[3],
                    DateOfBirth = dob,
                    Email = parts[5],
                    PhoneNumber = parts[6]
                };

                studentService.Add(student);
                Console.WriteLine("Student added.");
                continue;
            }

            // ========================= STUDENT GETALL ================================
            if (target.Equals("student", StringComparison.OrdinalIgnoreCase) &&
                action.Equals("getall", StringComparison.OrdinalIgnoreCase))
            {
                List<Student> allStudents = studentService.GetAll();

                if (allStudents.Count == 0)
                {
                    Console.WriteLine("No students found!");
                    continue;
                }

                foreach (var s in allStudents)
                {
                    Console.WriteLine($"{s.Id} - {s.Name} - {s.DateOfBirth} - {s.Email} - {s.PhoneNumber}");
                }

                continue;
            }

            // ========================= STUDENT REMOVE ================================
            if (target.Equals("student", StringComparison.OrdinalIgnoreCase) &&
                action.Equals("remove", StringComparison.OrdinalIgnoreCase))
            {
                if (parts.Length < 3)
                {
                    Console.WriteLine("Not enough parameters for student remove!");
                    continue;
                }

                string id = parts[2];
                studentService.DeleteById(id);
                Console.WriteLine("Student removed.");
                continue;
            }

            // ========================= STUDENT UPDATE ================================
            if (target.Equals("student", StringComparison.OrdinalIgnoreCase) &&
                action.Equals("update", StringComparison.OrdinalIgnoreCase))
            {
                if (parts.Length < 7)
                {
                    Console.WriteLine("Not enough parameters for student update!");
                    continue;
                }

                if (!DateOnly.TryParse(parts[4], out DateOnly dob))
                {
                    Console.WriteLine("Invalid date format!");
                    continue;
                }

                Student updatedStudent = new Student()
                {
                    Id = parts[2],
                    Name = parts[3],
                    DateOfBirth = dob,
                    Email = parts[5],
                    PhoneNumber = parts[6]
                };

                studentService.Update(updatedStudent);
                Console.WriteLine("Student updated.");
                continue;
            }
            // ========================= TEACHER ADD ================================
            if (target.Equals("teacher", StringComparison.OrdinalIgnoreCase) &&
                action.Equals("add", StringComparison.OrdinalIgnoreCase))
            {
                if (parts.Length < 7)
                {
                    Console.WriteLine("Not enough parameters for teacher add!");
                    continue;
                }

                if (!DateOnly.TryParse(parts[4], out DateOnly dob))
                {
                    Console.WriteLine("Invalid date format!");
                    continue;
                }

                Teacher teacher = new Teacher()
                {
                    Id = parts[2],
                    Name = parts[3],
                    DateOfBirth = dob,
                    Email = parts[5],
                    PhoneNumber = parts[6]
                };

                teacherService.Add(teacher);
                Console.WriteLine("Teacher added.");
                continue;
            }

            // ========================= TEACHER GETALL ================================
            if (target.Equals("teacher", StringComparison.OrdinalIgnoreCase) &&
                action.Equals("getall", StringComparison.OrdinalIgnoreCase))
            {
                List<Teacher> allTeachers = teacherService.GetAll();

                if (allTeachers.Count == 0)
                {
                    Console.WriteLine("No teachers found!");
                    continue;
                }

                foreach (var teacher in allTeachers)
                {
                    Console.WriteLine($"{teacher.Id} - {teacher.Name} - {teacher.DateOfBirth} - {teacher.Email} - {teacher.PhoneNumber}");
                }

                continue;
            }

            // ========================= TEACHER REMOVE ================================
            if (target.Equals("teacher", StringComparison.OrdinalIgnoreCase) &&
                action.Equals("remove", StringComparison.OrdinalIgnoreCase))
            {
                if (parts.Length < 3)
                {
                    Console.WriteLine("Not enough parameters for teacher remove!");
                    continue;
                }

                string id = parts[2];
                teacherService.DeleteById(id);
                Console.WriteLine("Teacher removed.");
                continue;
            }

            // ========================= TEACHER UPDATE ================================
            if (target.Equals("teacher", StringComparison.OrdinalIgnoreCase) &&
                action.Equals("update", StringComparison.OrdinalIgnoreCase))
            {
                if (parts.Length < 7)
                {
                    Console.WriteLine("Not enough parameters for teacher update!");
                    continue;
                }

                if (!DateOnly.TryParse(parts[4], out DateOnly dob))
                {
                    Console.WriteLine("Invalid date format!");
                    continue;
                }

                Teacher updatedTeacher = new Teacher()
                {
                    Id = parts[2],
                    Name = parts[3],
                    DateOfBirth = dob,
                    Email = parts[5],
                    PhoneNumber = parts[6]
                };

                teacherService.Update(updatedTeacher);
                Console.WriteLine("Teacher updated.");
                continue;

            }

            // ========================= COURSE ADD ================================
         if (target.Equals("course", StringComparison.OrdinalIgnoreCase) &&
                action.Equals("add", StringComparison.OrdinalIgnoreCase))
            {
                if (parts.Length < 5)
                {
                    Console.WriteLine("Not enough parameters for course add!");
                    continue;
                }

                string courseId = parts[2];
                string courseName = parts[3];
                string teacherId = parts[4];

                var studentIds = new List<string>();
                if (parts.Length > 5)
                {
                    for (int i = 5; i < parts.Length; i++)
                    {
                        string studentId = parts[i];
                        if (studentService.GetById(studentId) != null)
                        {
                            studentIds.Add(studentId);
                        }
                        else
                        {
                            Console.WriteLine($"Student {studentId} not found! Ignored.");
                        }
                    }
                }

                Course course = new Course()
                {
                    Id = courseId,
                    Name = courseName,
                    TeacherId = teacherId,
                    StudentIds = studentIds
                };

                courseService.Add(course);
                Console.WriteLine("Course added.");
                continue;
            } 
            // ========================= COURSE GETALL ================================
            if (target.Equals("course", StringComparison.OrdinalIgnoreCase) &&
                action.Equals("getall", StringComparison.OrdinalIgnoreCase))
            {
                List<Course> allCourses = courseService.GetAll();

                if (allCourses.Count == 0)
                {
                    Console.WriteLine("No courses found!");
                    continue;
                }

                foreach (var course in allCourses)
                {
                    Console.WriteLine($"{course.Id} - {course.Name} - {course.TeacherId} - {course.StudentIds.Count}");
                }

                continue;
            }

            // ========================= COURSE REMOVE ================================
            if (target.Equals("course", StringComparison.OrdinalIgnoreCase) &&
                action.Equals("remove", StringComparison.OrdinalIgnoreCase))
            {
                if (parts.Length < 3)
                {
                    Console.WriteLine("Not enough parameters for course remove!");
                    continue;
                }

                string id = parts[2];
                courseService.DeletById(id);
                Console.WriteLine("Course removed.");
                continue;
            }

            // ========================= COURSE UPDATE ================================
            if (target.Equals("course", StringComparison.OrdinalIgnoreCase) &&
                        action.Equals("update", StringComparison.OrdinalIgnoreCase))
            {
                if (parts.Length < 5)
                {
                    Console.WriteLine("Not enough parameters for course update!");
                    continue;
                }

                Course? oldCourse = courseService.GetById(parts[2]);
                if (oldCourse == null)
                {
                    Console.WriteLine("Course not found!");
                    continue;
                }

                string newName = parts[3];
                string newTeacherId = parts[4];

                List<string> newStudentIds = new List<string>();
                if (parts.Length > 5)
                {
                    for (int i = 5; i < parts.Length; i++)
                    {
                        string studentId = parts[i];
                        if (studentService.GetById(studentId) != null)
                        {
                            newStudentIds.Add(studentId);
                        }
                        else
                        {
                            Console.WriteLine($"Student {studentId} not found! Ignored.");
                        }
                    }
                }
                else
                {
                    // اگر دانشجو نداد، لیست قبلی رو نگه دار
                    newStudentIds = oldCourse.StudentIds;
                }

                Course updatedCourse = new Course()
                {
                    Id = parts[2],
                    Name = newName,
                    TeacherId = newTeacherId,
                    StudentIds = newStudentIds
                };

                courseService.Update(updatedCourse);
                Console.WriteLine("Course updated.");
                continue;

            }

        }
    }
}
