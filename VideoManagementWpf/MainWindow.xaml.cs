// Author: Sean Kelley
// Date Modified: 21 May 2013
// Assignment: C490 A1
// File: MainWindow.xaml.cs
// Purpose: To provide a WPF interface for managing videos.
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
        // Video information is stored into Library containers that are a collection of videos.
        private Library Movies, TVShows;
        const string MovieListBoxHeading = " --- Movies ------ ",
                     TVShowListBoxHeading = " --- TV Shows ---- ";

        // PersonalFavorites includes both TV Shows and Movies that I enjoy.
        // PersonalFavorites[0] is responsible for storing TV Shows, while
        // PersonalFavorites[1] is responsible for storing Movies.
        private readonly Video[][] PersonalFavorites = new Video[2][];

        public MainWindow()
        {
            InitializeComponent();

            // Create our libraries
            Movies = new Library(TypeOfVideo.Movie);
            TVShows = new Library(TypeOfVideo.TVShow);

            // Setup the ComboBox
            MovieTVShowComboBox.Items.Add(TypeOfVideo.Movie);
            MovieTVShowComboBox.Items.Add(TypeOfVideo.TVShow);
            MovieTVShowComboBox.SelectedIndex = 0;

            // TV Shows
            PersonalFavorites[0] = new Video[] {
                new Video("Planet Earth", 
                          "Documentary", 
                          "A voyage around the world with many remarkable sights", 
                          2006, TypeOfVideo.TVShow),
                new Video("Game of Thrones", 
                          "Fantasy", 
                          "Seven noble families fight for control of the mythical land of Westeros.", 
                          2011, TypeOfVideo.TVShow)
            };

            // Movies
            PersonalFavorites[1] = new Video[] {
                new Video("Django Unchained",
                          "Adventure",
                          "A freed slave sets out to rescue his wife from a brutal Mississippi plantation owner.",
                          2012, TypeOfVideo.Movie)
            };

            // Prepopulate with favorites
            for (int i = 0; i < PersonalFavorites[0].Count(); i++)
                TVShows.Add(ref PersonalFavorites[0][i]);
            for (int i = 0; i < PersonalFavorites[1].Count(); i++)
                Movies.Add(ref PersonalFavorites[1][i]);

            UpdateListBox();
        }

        /// <summary>
        /// Updates the ListBox so that it matches what are contained within the
        /// TVShows and Movies Libraries. Note that it always displays Movies first
        /// and displays single line (defined above) to denote which section.
        /// If data binding were used (which it should be), this function would be 
        /// largely useless, but it accomplishes the same task, albeit at the cost
        /// of reinitializing the ListBox upon every change.
        /// </summary>
        private void UpdateListBox()
        {
            LibraryListBox.Items.Clear();

            LibraryListBox.Items.Add(MovieListBoxHeading);
            foreach (Video movie in Movies.Videos)
                LibraryListBox.Items.Add(movie);

            LibraryListBox.Items.Add(TVShowListBoxHeading);
            foreach (Video tvShow in TVShows.Videos)
                LibraryListBox.Items.Add(tvShow);
        }

        /// <summary>
        /// This event corresponds with the pressing of the "Add Video" button. It
        /// attempts to add a video to the appropriate library based on information
        /// retrieved from imdbapi.org's API. It then calls UpdateListBox to display
        /// those results.
        /// </summary>
        private void AddVideoButton_Click(object sender, RoutedEventArgs e)
        {
            // Make sure both inputs are even present
            if (NewVideoTextBox.GetLineLength(0) == 0 || NewVideoYearTextBox.GetLineLength(0) == 0)
                return;

            // Convert the string for year to an unsigned short
            ushort year = Convert.ToUInt16(NewVideoYearTextBox.Text);

            try
            {
                // Attempt to add a new video using imdbapi.org to fill in details
                switch ((TypeOfVideo)MovieTVShowComboBox.SelectedItem)
                {
                    case TypeOfVideo.Movie:
                        Movies.AddVideoUsingImdb(NewVideoTextBox.Text, year);
                        break;
                    case TypeOfVideo.TVShow:
                        TVShows.AddVideoUsingImdb(NewVideoTextBox.Text, year);
                        break;
                }
            }
            catch
            {
                // Many exceptions could be thrown during the proccess of grabbing information from
                // imdbapi.org, so we will attempt to catch them here. Better error messages should
                // be added.
                MessageBox.Show("Error: Could not add " + NewVideoTextBox.Text + " (" + year + ").");
                return;
            }

            UpdateListBox();
        }

        /// <summary>
        /// This event corresponds with the action of selecting a ListBox item.
        /// Its purpose is to display information beyond just the year and title
        /// by filling in corresponding Labels and TextBlocks.
        /// </summary>
        /// <param name="sender">Corresponds with ListBox and is used to obtain which video is selected.</param>
        private void LibraryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Make sure the selected item isn't null, otherwise exceptions will be thrown when accessing it
            if (((ListBox)sender).SelectedItem == null)
                return;

            // Make sure a heading isn't selected
            if (((ListBox)sender).SelectedItem.GetType() != typeof(Video))
                return;

            // Get the contents of the selection
            Video selection = (Video)((ListBox)sender).SelectedItem;

            // Set the Labels to reflect the selection
            TitleContent.Content = selection.Title;
            YearContent.Content = selection.Year;
            GenreContent.Content = selection.Genre;
            DescriptionContent.Text = selection.Description;
        }

        /// <summary>
        /// Corresponds with "Remove Selection" button. It attempts to remove a video from
        /// its corresponding library based on which item in the ListBox is selected.
        /// </summary>
        private void RemoveSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            // Make sure there is something selected
            if (LibraryListBox.SelectedItem == null)
                return;

            // Make sure a heading isn't selected
            if (LibraryListBox.SelectedItem.GetType() != typeof(Video))
                return;

            // Find out which video is selected
            Video selection = (Video)LibraryListBox.SelectedItem;
            if (selection.Title != null)
            {
                // remove selection from appropriate Library
                switch (selection.Type)
                {
                    case TypeOfVideo.Movie:
                        Movies.Remove(selection.Title);
                        break;
                    case TypeOfVideo.TVShow:
                        TVShows.Remove(selection.Title);
                        break;
                }

                UpdateListBox();
            }
        }
    }
}
