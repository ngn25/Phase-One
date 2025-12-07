using System;
using System.Collections.Generic;
using firstpr;

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
        Console.WriteLine("  course getall | getbyid <id> | update <id> <newName> <newTeacherId> [studentId1 ...] | remove <id>");
        Console.WriteLine();

        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) continue;

            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
            {
                Console.WriteLine("Invalid command. Type at least two words (target action).");
                continue;
            }

            string target = parts[0].ToLower();
            string action = parts[1].ToLower();

            // ========================= STUDENT =========================
            if (target == "student")
            {
                if (action == "add")
                {
                    if (parts.Length < 7) { Console.WriteLine("Usage: student add <id> <name> <dob> <email> <phone>"); continue; }
                    if (!DateOnly.TryParse(parts[4], out DateOnly dob)) { Console.WriteLine("Invalid date format! Use yyyy-MM-dd"); continue; }

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

                if (action == "getall")
                {
                    var all = studentService.GetAll();
                    if (all.Count == 0) Console.WriteLine("No students found.");
                    else foreach (var s in all) Console.WriteLine(s);
                    continue;
                }

                if (action == "getbyid")
                {
                    if (parts.Length < 3) { Console.WriteLine("Usage: student getbyid <id>"); continue; }
                    var student = studentService.GetById(parts[2]);
                    Console.WriteLine(student == null ? $"Student not found: {parts[2]}" : student.ToString());
                    continue;
                }

                if (action == "update")
                {
                    if (parts.Length < 7) { Console.WriteLine("Usage: student update <id> <name> <dob> <email> <phone>"); continue; }
                    if (!DateOnly.TryParse(parts[4], out DateOnly dob)) { Console.WriteLine("Invalid date format!"); continue; }

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

                if (action == "remove")
                {
                    if (parts.Length < 3) { Console.WriteLine("Usage: student remove <id>"); continue; }
                    studentService.DeleteById(parts[2]);
                    Console.WriteLine("Student removed.");
                    continue;
                }
            }

            // ========================= TEACHER =========================
            if (target == "teacher")
            {
                if (action == "add")
                {
                    if (parts.Length < 7) { Console.WriteLine("Usage: teacher add <id> <name> <dob> <email> <phone>"); continue; }
                    if (!DateOnly.TryParse(parts[4], out DateOnly dob)) { Console.WriteLine("Invalid date format!"); continue; }

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

                if (action == "getall")
                {
                    var all = teacherService.GetAll();
                    if (all.Count == 0) Console.WriteLine("No teachers found.");
                    else foreach (var t in all) Console.WriteLine(t);
                    continue;
                }

                if (action == "getbyid")
                {
                    if (parts.Length < 3) { Console.WriteLine("Usage: teacher getbyid <id>"); continue; }
                    var teacher = teacherService.GetById(parts[2]);
                    Console.WriteLine(teacher == null ? $"Teacher not found: {parts[2]}" : teacher.ToString());
                    continue;
                }

                if (action == "update")
                {
                    if (parts.Length < 7) { Console.WriteLine("Usage: teacher update <id> <name> <dob> <email> <phone>"); continue; }
                    if (!DateOnly.TryParse(parts[4], out DateOnly dob)) { Console.WriteLine("Invalid date format!"); continue; }

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

                if (action == "remove")
                {
                    if (parts.Length < 3) { Console.WriteLine("Usage: teacher remove <id>"); continue; }
                    teacherService.DeleteById(parts[2]);
                    Console.WriteLine("Teacher removed.");
                    continue;
                }
            }

            // ========================= COURSE =========================
            if (target == "course")
            {
                // ADD
                if (action == "add")
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
                if (action == "getall")
                {
                    var all = courseService.GetAll();
                    if (all.Count == 0) Console.WriteLine("No courses found.");
                    else foreach (var c in all) Console.WriteLine(c);
                    continue;
                }

                // GETBYID
                if (action == "getbyid")
                {
                    if (parts.Length < 3) { Console.WriteLine("Usage: course getbyid <id>"); continue; }

                    var course = courseService.GetById(parts[2]);
                    if (course == null)
                    {
                        Console.WriteLine($"Course not found: {parts[2]}");
                        continue;
                    }

                    Console.WriteLine(course);

                    var teacher = teacherService.GetById(course.TeacherId);
                    Console.WriteLine($"Teacher: {(teacher != null ? $"{teacher.Name} ({teacher.Id})" : "Not Found")}");

                    Console.WriteLine($"Students ({course.StudentIds.Count}):");
                    if (course.StudentIds.Count == 0)
                        Console.WriteLine("  (No students enrolled)");
                    else
                        foreach (var sid in course.StudentIds)
                        {
                            var s = studentService.GetById(sid);
                            Console.WriteLine(s != null ? $"  - {s.Name} ({s.Id})" : $"  - {sid} (Deleted)");
                        }
                    continue;
                }

                // UPDATE
                if (action == "update")
                {
                    if (parts.Length < 5)
                    {
                        Console.WriteLine("Usage: course update <id> <newName> <newTeacherId> [studentId1 ...]");
                        continue;
                    }

                    string courseId = parts[2];
                    string newName = parts[3];
                    string newTeacherId = parts[4];

                    var oldCourse = courseService.GetById(courseId);
                    if (oldCourse == null)
                    {
                        Console.WriteLine("Course not found!");
                        continue;
                    }

                    var newStudentIds = new List<string>();
                    if (parts.Length > 5)
                    {
                        for (int i = 5; i < parts.Length; i++)
                        {
                            if (studentService.GetById(parts[i]) != null)
                                newStudentIds.Add(parts[i]);
                        }
                    }
                    else
                    {
                        newStudentIds = oldCourse.StudentIds;
                    }

                    var updatedCourse = new Course
                    {
                        Id = courseId,
                        Name = newName,
                        TeacherId = newTeacherId,
                        StudentIds = newStudentIds
                    };

                    courseService.Update(updatedCourse);
                    Console.WriteLine("Course updated successfully.");
                    continue;
                }

                // REMOVE
                if (action == "remove")
                {
                    if (parts.Length < 3) { Console.WriteLine("Usage: course remove <id>"); continue; }
                    courseService.DeleteById(parts[2]);
                    Console.WriteLine("Course removed.");
                    continue;
                }
            }

            Console.WriteLine("Unknown command. Try: student, teacher, course");
        }
    }
}