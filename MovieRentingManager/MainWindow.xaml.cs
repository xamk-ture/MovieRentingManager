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
        //Here we have the services that we will use to interact with the data
        //note that they are readonly, meaning that they cannot be changed after the constructor
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


        //TODO: Create new observable collections for Movies that is similar like the one for Books/Users
        //Note that you need to have a backing field for the property and the property itself
        //and the property has setter and getter like the one for Books/Users


        /// <summary>
        /// Backing field for books observable collection
        /// </summary>
        private ObservableCollection<Book> _books;

       /// <summary>
       /// Public property for the Books observable collection
       /// </summary>
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


        public MainWindow()
        {
            DataContext = this;

            _bookService = new BookService();
            _userService = new UserService();

            Books = new ObservableCollection<Book>(_bookService.GetBooks());
            Users = new ObservableCollection<User>(_userService.GetUsers());

            //TODO: Initialize the observable collections for Movies

            InitializeComponent();
        }

        private void Movie_Search_Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Change this to use your movie service

            int.TryParse(SearchMovieYearTextBox.Text, out int year);
            var books =_bookService.FindBook(SearchMovieTitleTextBox.Text, SearchMovieDirectorTextBox.Text, SearchMovieGenreTextBox.Text, year);

            Books = new ObservableCollection<Book>(books);
        }

        private void AddMovieButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddMovieDialog();


            //TODO: Change this to use your movie service and that it uses Movie object instead Book object

            if (dialog.ShowDialog() == true)
            {
                var book = dialog.NewBook;
                _bookService.AddBook(book);
                Books.Add(book);
            }
        }

        private void DeleteMovieButton_Click(object sender, RoutedEventArgs e)
        {

            //TODO: Change this to use Movie object instead of Book object and make it use your movie service
            //For example change this MoviesDataGrid.SelectedItem is not Book to MoviesDataGrid.SelectedItem is not Movie


            //this checks if the selected item is null or not a book and if the conditions are met it shows a dialog box with an error
            if (MoviesDataGrid.SelectedItem == null || MoviesDataGrid.SelectedItem is not Book)
            {
                //show dialog box with error
                MessageBox.Show("Please select a book to delete");
            }

            //convert/cast selected item to book type
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

        private void GetAllMovies_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Change this to use your movie service
            //it should look like this: Movies = new ObservableCollection<Movie>(_movieService.GetMovies());
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

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Implement edit user method
            //Hint check how 
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Implement delete user logic
        }

        private void EditMovieButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Change this to use Movie object instead of Book object and make it use your movie service

            if (MoviesDataGrid.SelectedItem == null || MoviesDataGrid.SelectedItem is not Book)
            {
                //show dialog box with error
                MessageBox.Show("Please select a book to delete");
            }

            Book book = (Book)MoviesDataGrid.SelectedItem;

            //show edit dialog
            EditMovieDialog dialog = new EditMovieDialog(book);
            dialog.ShowDialog();

            //update the books list so it shows the changes in ui
            Books = new ObservableCollection<Book>(Books);
        }
    }
}