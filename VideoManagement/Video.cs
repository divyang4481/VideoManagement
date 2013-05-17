using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoManagement
{
    // Videos will likely fall into two categories, TV Shows and Movies. It might
    // be helpful to keep track of their type.
    public enum TypeOfVideo { TVShow, Movie }

    /// <summary>
    /// Instances of the video class will contain basic information on the video and
    /// provide minimal ways of interacting with it.
    /// </summary>
    public class Video
    {
        private string Name;
        private string Genre;
        private string Description;
        private float Rating;

        public Video()
        {
        }

        public Video(string name, string genre, string description, float rating)
        {
            Name = name;
            Genre = genre;
            Description = description;
            Rating = rating;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
