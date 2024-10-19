using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    using System;
    using System.Collections.Generic;

    class Book
    {
        public string Title { get; }
        public string Author { get; }
        public string ISBN { get; }
        public int Copies { get; private set; }

        public Book(string title, string author, string isbn, int copies)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            Copies = copies;
        }

        public bool BorrowBook()
        {
            if (Copies > 0)
            {
                Copies--;
                return true;
            }
            return false;
        }

        public void ReturnBook()
        {
            Copies++;
        }
    }

    class Reader
    {
        public string Name { get; }
        public int ReaderId { get; }

        public Reader(string name, int readerId)
        {
            Name = name;
            ReaderId = readerId;
        }
    }

    class Library
    {
        private List<Book> books = new List<Book>();
        private List<Reader> readers = new List<Reader>();

        public void AddBook(Book book)
        {
            books.Add(book);
            Console.WriteLine($"Книга \"{book.Title}\" добавлена в библиотеку.");
        }

        public void RemoveBook(string isbn)
        {
            var book = books.Find(b => b.ISBN == isbn);
            if (book != null)
            {
                books.Remove(book);
                Console.WriteLine($"Книга \"{book.Title}\" удалена из библиотеки.");
            }
            else
            {
                Console.WriteLine("Книга не найдена.");
            }
        }

        public void RegisterReader(Reader reader)
        {
            readers.Add(reader);
            Console.WriteLine($"Читатель \"{reader.Name}\" зарегистрирован.");
        }

        public void RemoveReader(int readerId)
        {
            var reader = readers.Find(r => r.ReaderId == readerId);
            if (reader != null)
            {
                readers.Remove(reader);
                Console.WriteLine($"Читатель \"{reader.Name}\" удалён.");
            }
            else
            {
                Console.WriteLine("Читатель не найден.");
            }
        }

        public void BorrowBook(string isbn, int readerId)
        {
            var book = books.Find(b => b.ISBN == isbn);
            var reader = readers.Find(r => r.ReaderId == readerId);

            if (book != null && reader != null)
            {
                if (book.BorrowBook())
                {
                    Console.WriteLine($"Книга \"{book.Title}\" выдана читателю \"{reader.Name}\".");
                }
                else
                {
                    Console.WriteLine("Нет доступных экземпляров.");
                }
            }
            else
            {
                Console.WriteLine("Книга или читатель не найдены.");
            }
        }

        public void ReturnBook(string isbn)
        {
            var book = books.Find(b => b.ISBN == isbn);

            if (book != null)
            {
                book.ReturnBook();
                Console.WriteLine($"Книга \"{book.Title}\" возвращена в библиотеку.");
            }
            else
            {
                Console.WriteLine("Книга не найдена.");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Library library = new Library();

            Book book1 = new Book("Война и мир", "Лев Толстой", "123456789", 3);
            Book book2 = new Book("Преступление и наказание", "Фёдор Достоевский", "987654321", 2);
            library.AddBook(book1);
            library.AddBook(book2);

            Reader reader1 = new Reader("Иван Иванов", 1);
            Reader reader2 = new Reader("Мария Петрова", 2);
            library.RegisterReader(reader1);
            library.RegisterReader(reader2);

            library.BorrowBook("123456789", 1);
            library.ReturnBook("123456789");
            library.RemoveBook("987654321");
            library.RemoveReader(2);
        }
    }

}
