using System;
using System.Collections.Generic;
using System.Linq;

// Course class
public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }

    public Course(int id, string title, int credits)
    {
        Id = id;
        Title = title;
        Credits = credits;
    }
}

// Student class
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Course> Courses { get; set; } = new();

    public Student(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

// LMS App
public class LearningManagementApp
{
    public void Run()
    {
        // Seed courses
        var math = new Course(1, "Mathematics", 3);
        var physics = new Course(2, "Physics", 4);
        var programming = new Course(3, "Programming", 5);

        // Seed students
        var students = new List<Student>
        {
            new Student(1, "John Doe") { Courses = new List<Course> { math, physics } },
            new Student(2, "Jane Smith") { Courses = new List<Course> { programming } },
            new Student(3, "Alice Johnson") { Courses = new List<Course> { math, programming, physics } }
        };

        // 1️⃣ Filter students taking Physics
        var physicsStudents = students
            .Where(s => s.Courses.Any(c => c.Title == "Physics"))
            .ToList();

        Console.WriteLine("\nStudents enrolled in Physics:");
        foreach (var student in physicsStudents)
        {
            Console.WriteLine($" - {student.Name}");
        }

        // 2️⃣ Calculate average credits per student
        var avgCredits = students
            .Average(s => s.Courses.Sum(c => c.Credits));

        Console.WriteLine($"\nAverage credits per student: {avgCredits:F2}");
    }
}

class Program
{
    static void Main()
    {
        var app = new LearningManagementApp();
        app.Run();
    }
}
