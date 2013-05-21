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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoManagement;

namespace VideoManagementWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Library Movies;
        public MainWindow()
        {
            InitializeComponent();
            Movies = new Library(TypeOfVideo.Movie);
            Movies.Add("Django", "Comedy", "A great movie about a lizard", 9.8f);
            UpdateListBox();
        }

        private void UpdateListBox()
        {
            LibraryListBox.Items.Clear();
            foreach (Video video in Movies.Videos)
                LibraryListBox.Items.Add(video);
        }

        private void AddVideoButton_Click(object sender, RoutedEventArgs e)
        {
            ushort year = Convert.ToUInt16(NewVideoYearTextBox.Text);
            Movies.AddVideoUsingImdb(NewVideoTextBox.Text, year);
            UpdateListBox();
        }

        private void LibraryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the contents of the selection
            Video selection = (Video)((ListBox)sender).SelectedItem;
            if (selection != null)
            {
                TitleContent.Content = selection.Title;
                YearContent.Content = selection.Year;
                GenreContent.Content = selection.Genre;
                RatingContent.Content = selection.Rating;
                DescriptionContent.Text = selection.Description;
            }
        }

    }
}
