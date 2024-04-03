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

        List<Book> GetBooks();
    }
}
