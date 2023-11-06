using System;
using System.Collections.Generic;
using System.Linq;

// Interface for managing books
interface IBooks
{
    void AddBook();
    void DisplayAllBooks();
    void SearchBooks();
}

// Class for implementing book management
class Books : IBooks
{
    // Collection to store book details
    private List<Book> bookList = new List<Book>();

    public void AddBook()
    {
        try
        {
            Console.WriteLine("Enter Book ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Book Name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Book Category: ");
            string category = Console.ReadLine();

            Book newBook = new Book(id, name, category);
            bookList.Add(newBook);
            Console.WriteLine("Book added successfully!");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid number for the ID.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void GetBookDetails()
    {
        if (bookList.Count == 0)
        {
            Console.WriteLine("No books in the library.");
            return;
        }

        Console.WriteLine("Book Details:");
        foreach (var book in bookList)
        {
            Console.WriteLine($"Book ID: {book.Id}");
            Console.WriteLine($"Book Name: {book.Name}");
            Console.WriteLine($"Book Category: {book.Category}");
            Console.WriteLine();
        }
    }

    public void SearchBooks()
    {
        Console.WriteLine("Choose a search option:");
        Console.WriteLine("1. Search by ID");
        Console.WriteLine("2. Search by Name");
        Console.WriteLine("3. Search by Category");

        int searchOption;
        if (int.TryParse(Console.ReadLine(), out searchOption))
        {
            switch (searchOption)
            {
                case 1:
                    Console.WriteLine("Enter Book ID to search: ");
                    int searchId;
                    if (int.TryParse(Console.ReadLine(), out searchId))
                    {
                        var results = bookList.Where(book => book.Id == searchId);
                        DisplaySearchResults(results);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number for the ID.");
                    }
                    break;

                case 2:
                    Console.WriteLine("Enter Book Name to search: ");
                    string searchName = Console.ReadLine();
                    var nameResults = bookList.Where(book => book.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));
                    DisplaySearchResults(nameResults);
                    break;

                case 3:
                    Console.WriteLine("Enter Book Category to search: ");
                    string searchCategory = Console.ReadLine();
                    var categoryResults = bookList.Where(book => book.Category.Equals(searchCategory, StringComparison.OrdinalIgnoreCase));
                    DisplaySearchResults(categoryResults);
                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose a valid search option.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number for the search option.");
        }
    }

    private void DisplaySearchResults(IEnumerable<Book> results)
    {
        if (results.Any())
        {
            Console.WriteLine("Search Results:");
            foreach (var book in results)
            {
                Console.WriteLine($"Book ID: {book.Id}");
                Console.WriteLine($"Book Name: {book.Name}");
                Console.WriteLine($"Book Category: {book.Category}");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("No matching books found.");
        }
    }
}

// Class representing a book
class Book
{
    public int Id { get; }
    public string Name { get; }
    public string Category { get; }

    public Book(int id, string name, string category)
    {
        Id = id;
        Name = name;
        Category = category;
    }
}

class Program
{
    static void Main(string[] args)
    {
        IBooks library = new Books();
        while (true)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Get Book Details");
            Console.WriteLine("3. Search Books");
            Console.WriteLine("4. Exit");
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        library.AddBook();
                        break;
                    case 2:
                        library.GetBookDetails();
                        break;
                    case 3:
                        library.SearchBooks();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose a valid option.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number for the choice.");
            }
        }
    }
}
