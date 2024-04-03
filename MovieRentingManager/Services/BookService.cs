using MovieRentingManager.Interfaces;
using MovieRentingManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentingManager.Services
{
    public class BookService : IBookService
    {
        private List<Book> books = new List<Book>();

        public BookService()
        {
            books.Add(new Book
            {
                Id = 1,
                Title = "The Lord of the Rings",
                Author = "J.R.R. Tolkien",
                Genre = "Fantasy",
                Year = 1954,
                ISBN = "978-3-16-148410-0"
            });

            books.Add(new Book
            {
                Id = 2,
                Title = "Harry Potter and the Philosopher's Stone",
                Author = "J.K. Rowling",
                Genre = "Fantasy",
                Year = 1997,
                ISBN = "978-3-16-148410-1"
            });
        }

        public bool AddBook(Book book)
        {
            book.Id = GetNextId();
            books.Add(book);

            return true;
        }

        public List<Book> GetBooks()
        {
            return books;
        }

        public bool IsBookAvailable(int bookId)
        {
            Book book = books.FirstOrDefault(b => b.Id == bookId);

            if (book == null)
            {
                return false;
            }

            if (book.AvailableCopies == 0)
            {
                return false;
            }

            return true;
        }

        private int GetNextId()
        {
            return books.Count + 1;
        }
    }
}
