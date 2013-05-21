// Author: Sean Kelley
// Date Modified: 21 May 2013
// Assignment: C490 A1
// File: Video.cs
// Purpose: To provide a container for a single Video
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoManagement
{
    // Videos will likely fall into two categories, TV Shows and Movies.
    public enum TypeOfVideo { TVShow, Movie }

    /// <summary>
    /// Instances of the video class will contain basic information on the video and
    /// provide minimal ways of interacting with it.
    /// </summary>
    public class Video
    {
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public string Description { get; private set; }
        public ushort Year { get; private set; }
        public TypeOfVideo Type { get; private set; }

        private Video() { }

        /// <summary>
        /// The only public constructor available to the Video class. Must declare all relevant
        /// information at the time of instantiation.
        /// </summary>
        /// <param name="name">The Title or Name of a TV Show or Movie</param>
        /// <param name="genre">The Genre to which this video most belongs</param>
        /// <param name="description">A brief description or plot summary</param>
        /// <param name="year">The year in which the video was first released.
        /// For TV Shows, the year in which the first episode of season 1 aired.</param>
        /// <param name="type">The type of video. i.e. TypeOfVideo.TVShow or TypeOfVideo.Movie</param>
        public Video(string name, string genre, string description, ushort year, TypeOfVideo type)
        {
            Title = name;
            Genre = genre;
            Description = description;
            Year = year;
            Type = type;
        }

        /// <summary>
        /// Overrides built in ToString function to allow for easy textual representation of video objects.
        /// </summary>
        /// <returns>A string which briefly represents this video in the form: Tile Of Movie (Year)</returns>
        public override string ToString()
        {
            return Title + " (" + Year + ")";
        }
    }
}
