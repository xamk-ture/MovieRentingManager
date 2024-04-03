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
    /// Interaction logic for AddBookDialog.xaml
    /// </summary>
    public partial class AddMovieDialog : Window
    {
        public Book? NewBook { get; private set; }


        public AddMovieDialog()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Change this to use your Movie class
            NewBook = new Book
            {
                Title = titleTextBox.Text,
                Author = authorTextBox.Text,
                Genre = genreTextBox.Text,
                Year = int.TryParse(yearTextBox.Text, out int year) ? year : 0,
                AvailableCopies = int.TryParse(availableCopiesTextBox.Text, out int availableCopies) ? availableCopies : 0
            };

            this.DialogResult = true;
        }
    }
}
