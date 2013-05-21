// Author: Sean Kelley
// Date Modified: 21 May 2013
// Assignment: C490 A1
// File: Library.cs
// Purpose: To provide a container for Videos which handles adding and removing
//          by a variety of methods.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoManagement
{
    /// <summary>
    /// The Library class is meant to contain a number of videos of the same TypeOfVideo.
    /// It provides the basic functionality of adding and removing Videos, whether by supplying
    /// Video information manually, or attempting to use imdbapi.org to fill in that information.
    /// </summary>
    public class Library
    {
        // We will store our videos as a list of Videos
        public List<Video> Videos { get; private set; }
        // Each library should be either TypeOfVideo.TVShow or TypeOfVideo.Movie
        public TypeOfVideo Type { get; private set; }

        // Disallow public use of default constructor
        private Library()
        {
        }

        /// <summary>
        /// Create an empty library for storing videos of a specific type
        /// </summary>
        /// <param name="type">Denotes which type of video is stored in this library.</param>
        public Library(TypeOfVideo type)
        {
            Type = type;
            Videos = new List<Video>();
        }

        /// <summary>
        /// Returns the numbe of videos contained within this Library
        /// </summary>
        /// <returns>The number of videos contained within this Library</returns>
        public int Length()
        {
            return Videos.Count();
        }

        /// <summary>
        /// Add a video by supply all relevant information about it.
        /// </summary>
        /// <param name="name">Title of TV Show or Movie</param>
        /// <param name="genre">The genre of this video</param>
        /// <param name="description">A brief description or plot summary</param>
        /// <param name="year">The year in which this movie was first released.
        /// For TV Shows, The year that season 1 first aired</param>
        public void Add(string name, string genre, string description, ushort year)
        {
            Videos.Add(new Video(name, genre, description, year, Type));
        }
        
        /// <summary>
        /// Add a video to the library by using a video object.
        /// </summary>
        /// <param name="video">A reference to a video object. This object will not be modified.</param>
        public void Add(ref Video video)
        {
            Videos.Add(video);
        }

        /// <summary>
        /// Add multiple videos by supplying multiple video objects.
        /// </summary>
        /// <param name="videos">An array of multiple video objects to add to this Library.</param>
        public void Add(params Video[] videos)
        {
            foreach (Video video in videos)
                Add(video.Title, video.Genre, video.Description, video.Year);
        }

        /// <summary>
        /// Add a video to the library by specifying just the name and year. This will attempt
        /// to fill in other information through an api call to imdbapi.org. Exceptions may
        /// be raised in this process and this method does not handle them, so they must be caught
        /// by the caller.
        /// </summary>
        /// <param name="name">The Title of the Movie or TV Show</param>
        /// <param name="year">The Year that season 1 of the TV show first aired or the year of 
        /// release for a movie</param>
        public void AddVideoUsingImdb(string name, ushort year)
        {
            // Create a new Imdb object to handle interacting with imdbapi.org and receive the dictionary
            // it returns when finished.
            Imdb imdb = new Imdb(name, year);
            Dictionary<string,string> dict = imdb.GetJsonFromApi();

            // Using the dictionary returned, generate and add a new video
            Add(dict["title"], dict["genre"], dict["description"], year);
        }

        /// <summary>
        /// Attempts to remove a video by name.
        /// </summary>
        /// <param name="name">The name or title of a TV Show or Movie</param>
        /// <returns>The number of videos removed</returns>
        public int Remove(string name)
        {
            return Videos.RemoveAll(delegate(Video v) { return v.Title == name; });
        }

        /// <summary>
        /// Attempts to determine whether or not a video exists within the library.
        /// </summary>
        /// <param name="name">The name or title of a TV Show or Movie</param>
        /// <returns>True if the video exists, otherwise false.</returns>
        public bool Exists(string name)
        {
            return Videos.Exists(delegate(Video v) { return v.Title == name; });
        }

        /// <summary>
        /// Finds a video within the library and return a copy of it to the caller.
        /// </summary>
        /// <param name="name">The name or title of a TV Show or Movie</param>
        /// <returns>A copy of the video found or null if it was not found.</returns>
        public Video GetByName(string name)
        {
            return Videos.Find(delegate(Video v) { return v.Title == name; });
        }
    }
}
