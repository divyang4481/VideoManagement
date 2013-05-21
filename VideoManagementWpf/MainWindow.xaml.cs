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
        private Library Movies, TVShows;

        // PersonalFavorites includes both TV Shows and Movies that I enjoy.
        // PersonalFavorites[0] is responsible for storing TV Shows, while
        // PersonalFavorites[1] is responsible for storing Movies.
        private readonly Video[][] PersonalFavorites = new Video[2][];

        public MainWindow()
        {
            InitializeComponent();
            Movies = new Library(TypeOfVideo.Movie);
            TVShows = new Library(TypeOfVideo.TVShow);

            // TV Shows
            PersonalFavorites[0] = new Video[] {
                new Video("Planet Earth", 
                          "Documentary", 
                          "A voyage around the world with many remarkable sights", 
                          2006),
                new Video("Game of Thrones", 
                          "Fantasy", 
                          "Seven noble families fight for control of the mythical land of Westeros.", 
                          2011)
            };
            PersonalFavorites[1] = new Video[] {
                new Video("Django Unchained",
                          "Adventure",
                          "A freed slave sets out to rescue his wife from a brutal Mississippi plantation owner.",
                          2012)
            };

            foreach (Video tvShow in PersonalFavorites[0])
                TVShows.Add(tvShow);

            foreach (Video movie in PersonalFavorites[1])
                Movies.Add(movie);

            UpdateListBox();
        }

        private void UpdateListBox()
        {
            LibraryListBox.Items.Clear();

            foreach (Video movie in Movies.Videos)
                LibraryListBox.Items.Add(movie);
            foreach (Video tvShow in TVShows.Videos)
                LibraryListBox.Items.Add(tvShow);
        }

        private void AddVideoButton_Click(object sender, RoutedEventArgs e)
        {
            // Make sure both inputs are even present
            if (NewVideoTextBox.GetLineLength(0) == 0 || NewVideoYearTextBox.GetLineLength(0) == 0)
                return;

            // Convert the string for year to an unsigned short
            ushort year = Convert.ToUInt16(NewVideoYearTextBox.Text);

            // Many exceptions could be thrown during the proccess of grabbing information from
            // imdbapi.org, so we will attempt to catch them here.
            try
            {
                // Attempt to add a new video using imdbapi.org to fill in details
                Movies.AddVideoUsingImdb(NewVideoTextBox.Text, year);
            }
            catch
            {
                MessageBox.Show("Error: Could not add " + NewVideoTextBox.Text + " (" + year + ").");
                return;
            }

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
                DescriptionContent.Text = selection.Description;
            }
        }

        private void RemoveSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            Video selection = (Video)LibraryListBox.SelectedItem;
            if (selection != null)
            {
                Movies.Remove(selection.Title);
                UpdateListBox();
            }
        }
    }
}
