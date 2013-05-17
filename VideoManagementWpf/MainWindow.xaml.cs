﻿using System;
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
            Movies.Add(NewVideoTextBox.Text, "asdf", "fdsa", 5.0f, 1912);
            UpdateListBox();
        }

        private void LibraryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the contents of the selection
            Video selection = (Video)((ListBox)sender).SelectedItem;
            if (selection != null)
            {
                TitleContent.Content = selection.GetName();
                YearContent.Content = selection.GetYear();
                GenreContent.Content = selection.GetGenre();
                RatingContent.Content = selection.GetRating();
                DescriptionContent.Content = selection.GetDescription();
            }
        }

    }
}