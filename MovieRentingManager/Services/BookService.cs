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
            //Add some initial books
            books.Add(new Book
            {
                Id = 1,
                Title = "The Lord of the Rings",
                Author = "J.R.R. Tolkien",
                Genre = "Fantasy",
                Year = 1954,
            });

            books.Add(new Book
            {
                Id = 2,
                Title = "Harry Potter and the Philosopher's Stone",
                Author = "J.K. Rowling",
                Genre = "Fantasy",
                Year = 1997,
            });
        }

        public bool AddBook(Book book)
        {
            book.Id = GetNextId();
            books.Add(book);

            return true;
        }

        public IEnumerable<Book> FindBook(string title, string author, string genre, int? year)
        {
            var query = books.AsEnumerable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(b => b.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author.Contains(author));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(b => b.Genre.Contains(genre));
            }

            if (year != null)
            {
                query = query.Where(b => b.Year == year);
            }

            return query;
        }

        public Book? FindBook(int bookId)
        {
            return books.FirstOrDefault(b => b.Id == bookId);
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

        public bool RemoveBook(int bookId)
        {
            Book? bookToRemove = books.FirstOrDefault(b => b.Id == bookId);

            if(bookToRemove == null)
            {
                return false;
            }

            books.Remove(bookToRemove);

            return true;
        }

        private int GetNextId()
        {
            return books.Count + 1;
        }
    }
}
