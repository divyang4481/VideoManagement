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
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public string Description { get; private set; }
        public ushort Year { get; private set; }

        private Video()
        {
        }

        public Video(string name, string genre, string description, ushort year)
        {
            Title = name;
            Genre = genre;
            Description = description;
            Year = year;
        }

        public override string ToString()
        {
            return Title + " (" + Year + ")";
        }
    }
}
