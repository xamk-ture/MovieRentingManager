using MovieRentingManager.Interfaces;
using MovieRentingManager.Models;
using MovieRentingManager.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovieRentingManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private readonly IBookService _bookService;
        private readonly IUserService _userService;
        private readonly ILoanService _loanService;

        // Implement the INotifyPropertyChanged interface
        //No need to touch this
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to call when a property changes
        //No need to touch this
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Backing field for Books
        private ObservableCollection<Book> _books;

        // Public property for Books
        public ObservableCollection<Book> Books
        {
            get => _books;
            set
            {
                if (_books != value)
                {
                    _books = value;
                    OnPropertyChanged(nameof(Books)); // Notify that Books has changed
                }
            }
        }

        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                if (_users != value)
                {
                    _users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }
        }

        private ObservableCollection<LoanInfo> _loans;

        public ObservableCollection<LoanInfo> Loans
        {
            get => _loans;
            set
            {
                if (_loans != value)
                {
                    _loans = value;
                    OnPropertyChanged(nameof(Loans));
                }
            }
        }


        public MainWindow()
        {

            DataContext = this;


            _bookService = new BookService();
            _userService = new UserService();

            Books = new ObservableCollection<Book>(_bookService.GetBooks());

            InitializeComponent();
        }

        private void Movie_Search_Button_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(SearchMovieYearTextBox.Text, out int year);
            var books =_bookService.FindBook(SearchMovieTitleTextBox.Text, SearchMovieDirectorTextBox.Text, SearchMovieGenreTextBox.Text, year);

            Books = new ObservableCollection<Book>(books);
        }

        private void AddMovieButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddBookDialog();
            if (dialog.ShowDialog() == true)
            {
                var book = dialog.NewBook;
                _bookService.AddBook(book);
                Books.Add(book);
            }
        }

        private void DeleteMovieButton_Click(object sender, RoutedEventArgs e)
        {
            if(MoviesDataGrid.SelectedItem == null || MoviesDataGrid.SelectedItem is not Book)
            {
                //show dialog box with error
                MessageBox.Show("Please select a book to delete");
            }

            //convert selected item to book type
            Book book = (Book)MoviesDataGrid.SelectedItem;

            //tries to remove book and returns true if successful
            var removeWasSucceess = _bookService.RemoveBook(book.Id);

            if(removeWasSucceess)
            {
                Books.Remove(book);
            }
            else
            {
                //show dialog box with error
                MessageBox.Show("Book could not be deleted");
            }
        }

        private void GetAllBooks_Click(object sender, RoutedEventArgs e)
        {
            Books = new ObservableCollection<Book>(_bookService.GetBooks());
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddUserDialog();
            if (dialog.ShowDialog() == true)
            {
                var user = dialog.NewUser;
                _userService.AddUser(user);
                Users.Add(user);
            }
        }
    }
}