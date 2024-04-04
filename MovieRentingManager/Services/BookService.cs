using MovieRentingManager.Interfaces;
using MovieRentingManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public IEnumerable<Book> FindBook(string title, string director, string genre, int? year)
        {
            //Build a new queryable object from the list of books
            IQueryable<Book> query = books.AsQueryable();

            //Add query filter based on the input parameters
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(b => b.Title.ToLower().Contains(title.ToLower()));
            }

            if (!string.IsNullOrEmpty(director))
            {
                query = query.Where(b => b.Author.ToLower().Contains(director.ToLower()));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(b => b.Genre.ToLower().Contains(genre.ToLower()));
            }

            if (year != null && year > 0)
            {
                query = query.Where(b => b.Year == year);
            }

            return query.ToList();
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
            Book? book = books.FirstOrDefault(b => b.Id == bookId);

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
            return books.Max(x => x.Id) + 1;
        }
    }
}
