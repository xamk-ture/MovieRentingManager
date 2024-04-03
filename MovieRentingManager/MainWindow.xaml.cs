using MovieRentingManager.Interfaces;
using MovieRentingManager.Models;
using MovieRentingManager.Services;
using System.Collections.ObjectModel;
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
    public partial class MainWindow : Window
    {

        private readonly IBookService _bookService;

        public ObservableCollection<Book> Books { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            _bookService = new BookService();

            Books = new ObservableCollection<Book>(_bookService.GetBooks());

            MoviesDataGrid.ItemsSource = Books;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Movie_Search_Button_Click(object sender, RoutedEventArgs e)
        {

            //Here movie search logic
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
    }
}