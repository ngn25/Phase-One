using System;
using System.Collections.Generic;
using firstpr; // مطمئن شو این namespace درست باشه

class Program
{
    static void Main()
    {
        var studentService = new StudentService(new StudentRepository());
        var teacherService = new TeacherService(new TeacherRepository());
        var courseService = new CourseService(studentService, teacherService, new CourseRepository());

        Console.WriteLine("=== School Management Console ===");
        Console.WriteLine("Commands:");
        Console.WriteLine("  student add <id> <name> <dob:yyyy-MM-dd> <email> <phone>");
        Console.WriteLine("  student getall | getbyid <id> | update <id> <name> <dob> <email> <phone> | remove <id>");
        Console.WriteLine("  teacher add/getall/getbyid/update/remove (same as student)");
        Console.WriteLine("  course add <id> <name> <teacherId> [studentId1 studentId2 ...]");
        Console.WriteLine("  course getall | getbyid <id> | update <id> <name> <teacherId> [studentId1 ...] | remove <id>");
        Console.WriteLine();

        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                continue;

            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
            {
                Console.WriteLine("Invalid command. Type at least two words (target action).");
                continue;
            }

            string target = parts[0];
            string action = parts[1];

            // ========================= STUDENT =========================
            if (target.Equals("student", StringComparison.OrdinalIgnoreCase))
            {
                // ADD
                if (action.Equals("add", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length < 7)
                    {
                        Console.WriteLine("Usage: student add <id> <name> <dob:yyyy-MM-dd> <email> <phone>");
                        continue;
                    }
                    if (!DateOnly.TryParse(parts[4], out DateOnly dob))
                    {
                        Console.WriteLine("Invalid date format! Use yyyy-MM-dd");
                        continue;
                    }

                    var student = new Student
                    {
                        Id = parts[2],
                        Name = parts[3],
                        DateOfBirth = dob,
                        Email = parts[5],
                        PhoneNumber = parts[6]
                    };

                    studentService.Add(student);
                    Console.WriteLine("Student added successfully.");
                    continue;
                }

                // GETALL
                if (action.Equals("getall", StringComparison.OrdinalIgnoreCase))
                {
                    var all = studentService.GetAll();
                    if (all.Count == 0) Console.WriteLine("No students found.");
                    else
                        foreach (var s in all)
                            Console.WriteLine($"{s.Id} | {s.Name} | {s.DateOfBirth:yyyy-MM-dd} | {s.Email} | {s.PhoneNumber}");
                    continue;
                }

                // GETBYID
                if (action.Equals("getbyid", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length < 3)
                    {
                        Console.WriteLine("Usage: student getbyid <id>");
                        continue;
                    }
                    var student = studentService.GetById(parts[2]);
                    if (student == null)
                        Console.WriteLine($"Student with id '{parts[2]}' not found.");
                    else
                    {
                        Console.WriteLine("=== Student Details ===");
                        Console.WriteLine($"Id          : {student.Id}");
                        Console.WriteLine($"Name        : {student.Name}");
                        Console.WriteLine($"Date of Birth: {student.DateOfBirth:yyyy-MM-dd}");
                        Console.WriteLine($"Email       : {student.Email}");
                        Console.WriteLine($"Phone       : {student.PhoneNumber}");
                    }
                    continue;
                }

                // UPDATE
                if (action.Equals("update", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length < 7)
                    {
                        Console.WriteLine("Usage: student update <id> <name> <dob:yyyy-MM-dd> <email> <phone>");
                        continue;
                    }
                    if (!DateOnly.TryParse(parts[4], out DateOnly dob))
                    {
                        Console.WriteLine("Invalid date format!");
                        continue;
                    }

                    var updated = new Student
                    {
                        Id = parts[2],
                        Name = parts[3],
                        DateOfBirth = dob,
                        Email = parts[5],
                        PhoneNumber = parts[6]
                    };

                    studentService.Update(updated);
                    Console.WriteLine("Student updated successfully.");
                    continue;
                }

                // REMOVE
                if (action.Equals("remove", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length < 3)
                    {
                        Console.WriteLine("Usage: student remove <id>");
                        continue;
                    }
                    studentService.DeleteById(parts[2]);
                    Console.WriteLine("Student removed (if existed).");
                    continue;
                }
            }

            // ========================= TEACHER =========================
            if (target.Equals("teacher", StringComparison.OrdinalIgnoreCase))
            {
                if (action.Equals("add", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length < 7)
                    {
                        Console.WriteLine("Usage: teacher add <id> <name> <dob:yyyy-MM-dd> <email> <phone>");
                        continue;
                    }
                    if (!DateOnly.TryParse(parts[4], out DateOnly dob))
                    {
                        Console.WriteLine("Invalid date format!");
                        continue;
                    }

                    var teacher = new Teacher
                    {
                        Id = parts[2],
                        Name = parts[3],
                        DateOfBirth = dob,
                        Email = parts[5],
                        PhoneNumber = parts[6]
                    };

                    teacherService.Add(teacher);
                    Console.WriteLine("Teacher added successfully.");
                    continue;
                }

                if (action.Equals("getall", StringComparison.OrdinalIgnoreCase))
                {
                    var all = teacherService.GetAll();
                    if (all.Count == 0) Console.WriteLine("No teachers found.");
                    else
                        foreach (var t in all)
                            Console.WriteLine($"{t.Id} | {t.Name} | {t.DateOfBirth:yyyy-MM-dd} | {t.Email} | {t.PhoneNumber}");
                    continue;
                }

                if (action.Equals("getbyid", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length < 3)
                    {
                        Console.WriteLine("Usage: teacher getbyid <id>");
                        continue;
                    }
                    var teacher = teacherService.GetById(parts[2]);
                    if (teacher == null)
                        Console.WriteLine($"Teacher with id '{parts[2]}' not found.");
                    else
                    {
                        Console.WriteLine("=== Teacher Details ===");
                        Console.WriteLine($"Id          : {teacher.Id}");
                        Console.WriteLine($"Name        : {teacher.Name}");
                        Console.WriteLine($"Date of Birth: {teacher.DateOfBirth:yyyy-MM-dd}");
                        Console.WriteLine($"Email       : {teacher.Email}");
                        Console.WriteLine($"Phone       : {teacher.PhoneNumber}");
                    }
                    continue;
                }

                if (action.Equals("update", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length < 7)
                    {
                        Console.WriteLine("Usage: teacher update <id> <name> <dob:yyyy-MM-dd> <email> <phone>");
                        continue;
                    }
                    if (!DateOnly.TryParse(parts[4], out DateOnly dob))
                    {
                        Console.WriteLine("Invalid date format!");
                        continue;
                    }

                    var updated = new Teacher
                    {
                        Id = parts[2],
                        Name = parts[3],
                        DateOfBirth = dob,
                        Email = parts[5],
                        PhoneNumber = parts[6]
                    };

                    teacherService.Update(updated);
                    Console.WriteLine("Teacher updated successfully.");
                    continue;
                }

                if (action.Equals("remove", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length < 3)
                    {
                        Console.WriteLine("Usage: teacher remove <id>");
                        continue;
                    }
                    teacherService.DeleteById(parts[2]);
                    Console.WriteLine("Teacher removed (if existed).");
                    continue;
                }
            }

            // ========================= COURSE =========================
            if (target.Equals("course", StringComparison.OrdinalIgnoreCase))
            {
                // ADD
                if (action.Equals("add", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length < 5)
                    {
                        Console.WriteLine("Usage: course add <id> <name> <teacherId> [studentId1 studentId2 ...]");
                        continue;
                    }

                    string courseId = parts[2];
                    string courseName = parts[3];
                    string teacherId = parts[4];

                    var studentIds = new List<string>();
                    for (int i = 5; i < parts.Length; i++)
                    {
                        if (studentService.GetById(parts[i]) != null)
                            studentIds.Add(parts[i]);
                        else
                            Console.WriteLine($"Student {parts[i]} not found and ignored.");
                    }

                    var course = new Course
                    {
                        Id = courseId,
                        Name = courseName,
                        TeacherId = teacherId,
                        StudentIds = studentIds
                    };

                    courseService.Add(course);
                    Console.WriteLine("Course added successfully.");
                    continue;
                }

                // GETALL
                if (action.Equals("getall", StringComparison.OrdinalIgnoreCase))
                {
                    var all = courseService.GetAll();
                    if (all.Count == 0) Console.WriteLine("No courses found.");
                    else
                        foreach (var c in all)
                            Console.WriteLine($"{c.Id} | {c.Name} | Teacher: {c.TeacherId} | Students: {c.StudentIds.Count}");
                    continue;
                }

                // GETBYID
                if (action.Equals("getbyid", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length < 3)
                    {
                        Console.WriteLine("Usage: course getbyid <id>");
                        continue;
                    }

                    var course = courseService.GetById(parts[2]);
                    if (course == null)
                    {
                        Console.WriteLine($"Course with id '{parts[2]}' not found.");
                        continue;
                    }

                    var teacher = teacherService.GetById(course.TeacherId);
                    string teacherInfo = teacher != null ? $"{teacher.Name} ({teacher.Id})" : $"{course.TeacherId} (Not Found!)";

                    Console.WriteLine("=== Course Details ===");
                    Console.WriteLine($"Id       : {course.Id}");
                    Console.WriteLine($"Name     : {course.Name}");
                    Console.WriteLine($"Teacher  : {teacherInfo}");
                    Console.WriteLine($"Students ({course.StudentIds.Count}):");

                    if (course.StudentIds.Count == 0)
                        Console.WriteLine("  (No students enrolled)");
                    else
                        foreach (var sid in course.StudentIds)
                        {
                            var s = studentService.GetById(sid);
                            string info = s != null ? $"{s.Name} ({s.Id})" : $"{sid} (Not Found!)";
                            Console.WriteLine($"  - {info}");
                        }
                    continue;
                }

                // UPDATE
                if (action.Equals("update", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length < 5)
                    {
                        Console.WriteLine("Usage: course update <id> <newName> <newTeacherId> [studentId1 studentId2 ...]");
                        continue;
                    }

                    var oldCourse = courseService.GetById(parts[2]);
                    if (oldCourse == null)
                    {
                        Console.WriteLine("Course not found!");
                        continue;
                    }

                    string newName = parts[3];
                    string newTeacherId = parts[4];

                    var newStudentIds = new List<string>();
                    if (parts.Length > 5)
                    {
                        for (int i = 5; i < parts.Length; i++)
                        {
                            if (studentService.GetById(parts[i]) != null)
                                newStudentIds.Add(parts[i]);
                            else
                                Console.WriteLine($"Student {parts[i]} not found and ignored.");
                        }
                    }
                    else
                    {
                        newStudentIds = oldCourse.StudentIds; 
                    }

                    var updatedCourse = new Course
                    {
                        Id = parts[2],
                        Name = newName,
                        TeacherId = newTeacherId,
                        StudentIds = newStudentIds
                    };

                    courseService.Update(updatedCourse);
                    Console.WriteLine("Course updated successfully.");
                    continue;
                }

                // REMOVE
                if (action.Equals("remove", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length < 3)
                    {
                        Console.WriteLine("Usage: course remove <id>");
                        continue;
                    }
                    courseService.DeletById(parts[2]); 
                    Console.WriteLine("Course removed (if existed).");
                    continue;
                }
            }

            Console.WriteLine("Unknown command. Type 'student', 'teacher' or 'course' followed by an action.");
        }
    }
}