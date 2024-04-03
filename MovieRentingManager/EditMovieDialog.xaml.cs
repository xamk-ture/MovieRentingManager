using MovieRentingManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MovieRentingManager
{
    /// <summary>
    /// Interaction logic for EditMovieDialog.xaml
    /// </summary>
    public partial class EditMovieDialog : Window
    {
        private readonly Book _book;   

        public EditMovieDialog(Book book)
        {
            _book = book;

            InitializeComponent();

            titleTextBox.Text = book.Title;
            authorTextBox.Text = book.Author;
            genreTextBox.Text = book.Genre;
            yearTextBox.Text = book.Year.ToString();
            availableCopiesTextBox.Text = book.AvailableCopies.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _book.Title = titleTextBox.Text;
            _book.Author = authorTextBox.Text;
            _book.Genre = genreTextBox.Text;
            _book.Year = int.TryParse(yearTextBox.Text, out int year) ? year : 0;
            _book.AvailableCopies = int.TryParse(availableCopiesTextBox.Text, out int availableCopies) ? availableCopies : 0;

            this.DialogResult = true;
        }
    }
}
