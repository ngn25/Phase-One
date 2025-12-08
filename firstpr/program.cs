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
        Console.WriteLine("  teacher add <id> <name> <email> <phone>");
        Console.WriteLine("  teacher getall | getbyid <id> | update <id> <name> <email> <phone> | remove <id>");
        Console.WriteLine("  course add <id> <name> <teacherId> [studentId1 studentId2 ...]");
        Console.WriteLine("  course getall | getbyid <id> | update <id> <newName> <newTeacherId> [studentIds...]");
        Console.WriteLine("  Type 'help' for this list, 'exit' to quit.");
        Console.WriteLine();

        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input)) continue;

            if (input.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Goodbye!");
                break;
            }

            if (input.Trim().Equals("help", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Available commands: student, teacher, course");
                Console.WriteLine("Type 'exit' to close the program.");
                continue;
            }

            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
            {
                Console.WriteLine("Invalid command. Use: target action [parameters]");
                continue;
            }

            string target = parts[0].ToLower();
            string action = parts[1].ToLower();

            // ========================= STUDENT =========================
            if (target == "student")
            {
                if (action == "add")
                {
                    if (parts.Length < 7) { Console.WriteLine("Usage: student add <id> <name> <dob:yyyy-MM-dd> <email> <phone>"); continue; }
                    if (!DateOnly.TryParse(parts[4], out DateOnly dob)) { Console.WriteLine("Invalid date format! Use yyyy-MM-dd"); continue; }

                    studentService.Add(new Student
                    {
                        Id = parts[2],
                        Name = parts[3],
                        DateOfBirth = dob,
                        Email = parts[5],
                        PhoneNumber = parts[6]
                    });
                    Console.WriteLine("Student added successfully.");
                    continue;
                }

                if (action == "getall")
                {
                    var list = studentService.GetAll();
                    if (list.Count == 0) Console.WriteLine("No students found.");
                    else foreach (var s in list) Console.WriteLine(s);
                    continue;
                }

                if (action == "getbyid")
                {
                    if (parts.Length < 3) { Console.WriteLine("Usage: student getbyid <id>"); continue; }
                    var s = studentService.GetById(parts[2]);
                    Console.WriteLine(s == null ? $"Student not found: {parts[2]}" : s.ToString());
                    continue;
                }

                if (action == "update")
                {
                    if (parts.Length < 7) { Console.WriteLine("Usage: student update <id> <name> <dob> <email> <phone>"); continue; }
                    if (!DateOnly.TryParse(parts[4], out DateOnly dob)) { Console.WriteLine("Invalid date format!"); continue; }

                    studentService.Update(new Student
                    {
                        Id = parts[2],
                        Name = parts[3],
                        DateOfBirth = dob,
                        Email = parts[5],
                        PhoneNumber = parts[6]
                    });
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
            else if (target == "teacher")
            {
                if (action == "add")
                {
                    if (parts.Length < 6) { Console.WriteLine("Usage: teacher add <id> <name> <email> <phone>"); continue; }

                    teacherService.Add(new Teacher
                    {
                        Id = parts[2],
                        Name = parts[3],
                        Email = parts[4],
                        PhoneNumber = parts[5]
                    });
                    Console.WriteLine("Teacher added successfully.");
                    continue;
                }

                if (action == "getall")
                {
                    var list = teacherService.GetAll();
                    if (list.Count == 0) Console.WriteLine("No teachers found.");
                    else foreach (var t in list) Console.WriteLine(t);
                    continue;
                }

                if (action == "getbyid")
                {
                    if (parts.Length < 3) { Console.WriteLine("Usage: teacher getbyid <id>"); continue; }
                    var t = teacherService.GetById(parts[2]);
                    Console.WriteLine(t == null ? $"Teacher not found: {parts[2]}" : t.ToString());
                    continue;
                }

                if (action == "update")
                {
                    if (parts.Length < 6) { Console.WriteLine("Usage: teacher update <id> <name> <email> <phone>"); continue; }

                    teacherService.Update(new Teacher
                    {
                        Id = parts[2],
                        Name = parts[3],
                        Email = parts[4],
                        PhoneNumber = parts[5]
                    });
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
            else if (target == "course")
            {
                if (action == "add")
                {
                    if (parts.Length < 5) { Console.WriteLine("Usage: course add <id> <name> <teacherId> [studentIds...]"); continue; }

                    string teacherId = parts[4];
                    if (teacherService.GetById(teacherId) == null)
                    {
                        Console.WriteLine($"Error: Teacher with ID '{teacherId}' does not exist!");
                        continue;
                    }

                    var studentIds = new List<string>();
                    for (int i = 5; i < parts.Length; i++)
                        if (studentService.GetById(parts[i]) != null)
                            studentIds.Add(parts[i]);

                    courseService.Add(new Course
                    {
                        Id = parts[2],
                        Name = parts[3],
                        TeacherId = teacherId,
                        StudentIds = studentIds
                    });
                    Console.WriteLine("Course added successfully.");
                    continue;
                }

                if (action == "getall")
                {
                    var list = courseService.GetAll();
                    if (list.Count == 0) Console.WriteLine("No courses found.");
                    else foreach (var c in list) Console.WriteLine(c);
                    continue;
                }

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
                    Console.WriteLine($"Teacher: {(teacher != null ? $"{teacher.Name} ({teacher.Id})" : "Not found")}");

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

                if (action == "update")
                {
                    if (parts.Length < 5) { Console.WriteLine("Usage: course update <id> <newName> <newTeacherId> [studentIds...]"); continue; }

                    string newTeacherId = parts[4];
                    if (teacherService.GetById(newTeacherId) == null)
                    {
                        Console.WriteLine($"Error: Teacher with ID '{newTeacherId}' does not exist!");
                        continue;
                    }

                    var old = courseService.GetById(parts[2]);
                    if (old == null) { Console.WriteLine("Course not found!"); continue; }

                    var newStudentIds = parts.Length > 5
                        ? parts.Skip(5).Where(id => studentService.GetById(id) != null).ToList()
                        : old.StudentIds;

                    courseService.Update(new Course
                    {
                        Id = parts[2],
                        Name = parts[3],
                        TeacherId = newTeacherId,
                        StudentIds = newStudentIds
                    });
                    Console.WriteLine("Course updated successfully.");
                    continue;
                }

                if (action == "remove")
                {
                    if (parts.Length < 3) { Console.WriteLine("Usage: course remove <id>"); continue; }
                    courseService.DeleteById(parts[2]);
                    Console.WriteLine("Course removed.");
                    continue;
                }
            }

            Console.WriteLine("Unknown command. Type 'help' for available commands.");
        }
    }
}