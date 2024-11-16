using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    class Book
    {
        public string Title { get; }
        public string Author { get; }
        public string ISBN { get; }
        public bool IsAvailable { get; set; } = true;

        public Book(string title, string author, string isbn)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
        }
    }

    class Reader
    {
        public string Name { get; }
        private List<Book> RentedBooks { get; } = new();

        public Reader(string name) => Name = name;

        public bool RentBook(Book book)
        {
            if (!book.IsAvailable) return false;
            book.IsAvailable = false;
            RentedBooks.Add(book);
            return true;
        }

        public void ReturnBook(Book book)
        {
            if (RentedBooks.Remove(book))
                book.IsAvailable = true;
        }

        public IEnumerable<Book> GetRentedBooks() => RentedBooks;
    }

    class Librarian
    {
        public string Name { get; }
        public Librarian(string name) => Name = name;

        public void AddBook(Library library, Book book) => library.AddBook(book);

        public void RemoveBook(Library library, Book book) => library.RemoveBook(book);
    }

    class Library
    {
        private List<Book> Books { get; } = new();

        public void AddBook(Book book) => Books.Add(book);

        public void RemoveBook(Book book) => Books.Remove(book);

        public IEnumerable<Book> SearchByTitle(string title) =>
            Books.Where(book => book.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<Book> SearchByAuthor(string author) =>
            Books.Where(book => book.Author.Contains(author, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<Book> GetAvailableBooks() => Books.Where(book => book.IsAvailable);

        public IEnumerable<Book> GetAllBooks() => Books;
    }
    class Program
    {
        static void Main(string[] args)
        {
            var library = new Library();
            var librarian = new Librarian("Anna");
            var reader = new Reader("John");

            librarian.AddBook(library, new Book("Book A", "Author A", "12345"));
            librarian.AddBook(library, new Book("Book B", "Author B", "67890"));

            var book = library.GetAvailableBooks().First();
            reader.RentBook(book);

            Console.WriteLine("Available Books:");
            foreach (var availableBook in library.GetAvailableBooks())
                Console.WriteLine($"{availableBook.Title} by {availableBook.Author}");

            Console.WriteLine("\nRented Books:");
            foreach (var rentedBook in reader.GetRentedBooks())
                Console.WriteLine($"{rentedBook.Title} by {rentedBook.Author}");

        }
    }
}
