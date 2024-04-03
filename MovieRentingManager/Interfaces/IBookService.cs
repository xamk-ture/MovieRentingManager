using MovieRentingManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentingManager.Interfaces
{
    public interface IBookService
    {
        bool IsBookAvailable(int bookId);

        bool AddBook(Book book);

        bool RemoveBook(int bookId);

        IEnumerable<Book> FindBook(string title, string author, string genre, int? year);

        Book? FindBook(int bookId);

        List<Book> GetBooks();
    }
}
