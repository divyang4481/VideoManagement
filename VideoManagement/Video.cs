using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoManagement
{
    // Videos will likely fall into two categories, TV Shows and Movies. It might
    // help to keep track of their type.
    public enum TypeOfVideo { TVShow, Movie }

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
