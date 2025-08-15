using System;
using System.Collections.Generic;
using System.Linq;

// Book class
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }

    public Book(int id, string title, string author)
    {
        Id = id;
        Title = title;
        Author = author;
    }
}

// Member class
public class Member
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Member(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

// Borrowing class
public class Borrowing
{
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateTime BorrowDate { get; set; }

    public Borrowing(int bookId, int memberId, DateTime borrowDate)
    {
        BookId = bookId;
        MemberId = memberId;
        BorrowDate = borrowDate;
    }
}

// Library Management App
public class LibraryApp
{
    public void Run()
    {
        // Seed books
        var books = new List<Book>
        {
            new Book(1, "C# Programming", "John Smith"),
            new Book(2, "Data Structures", "Jane Doe"),
            new Book(3, "Database Systems", "Alice Johnson")
        };

        // Seed members
        var members = new List<Member>
        {
            new Member(1, "Michael Brown"),
            new Member(2, "Sarah Davis")
        };

        // Seed borrowings
        var borrowings = new List<Borrowing>
        {
            new Borrowing(1, 1, DateTime.Now.AddDays(-5)),
            new Borrowing(3, 2, DateTime.Now.AddDays(-2))
        };

        // Join borrowings with members and books
        var borrowingReport = from borrow in borrowings
                              join book in books on borrow.BookId equals book.Id
                              join member in members on borrow.MemberId equals member.Id
                              select new
                              {
                                  MemberName = member.Name,
                                  BookTitle = book.Title,
                                  Author = book.Author,
                                  BorrowDate = borrow.BorrowDate
                              };

        // Display report
        Console.WriteLine("\n--- Library Borrowing Report ---");
        Console.WriteLine($"{"Member",-20} {"Book",-25} {"Author",-20} {"Borrow Date",-15}");
        Console.WriteLine(new string('-', 80));

        foreach (var record in borrowingReport)
        {
            Console.WriteLine($"{record.MemberName,-20} {record.BookTitle,-25} {record.Author,-20} {record.BorrowDate.ToShortDateString(),-15}");
        }
    }
}

class Program
{
    static void Main()
    {
        var app = new LibraryApp();
        app.Run();
    }
}
