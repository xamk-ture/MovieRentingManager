using MovieRentingManager.Interfaces;
using MovieRentingManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentingManager.Services
{
    public class BookLoanService : ILoanService
    {
        private readonly IBookService bookService;
        private List<LoanInfo> loans = new List<LoanInfo>();

        public BookLoanService(IBookService service)
        {
            bookService = service;
        }

        public bool Loan(LoanInfo loan)
        {
            Book book = bookService.FindBook(loan.BookId);

            if (bookService.IsBookAvailable(book.Id) == false)
                return false;

            book.AvailableCopies--;

            loans.Add(loan);

            return true;
        }

        public bool ReturnLoan(int loanId)
        {
            LoanInfo? loanInfo = loans.FirstOrDefault(l => l.Id == loanId);

            if (loanInfo == null)
                return false;

            Book book = bookService.FindBook(loanInfo.BookId);
            book.AvailableCopies++;

            return true;
        }

        public bool UpdateLoan(LoanInfo loan)
        {
            throw new NotImplementedException();
        }
    }
}
