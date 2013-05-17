using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoManagement
{
    /// <summary>
    /// The Library class is meant to contain a number of videos of the same TypeOfVideo.
    /// </summary>
    public class Library
    {
        private List<Video> Videos;
        TypeOfVideo Type;

        // Create an empty library of a specific type, i.e. TypeOfVideo.Movie
        public Library(TypeOfVideo type)
        {
            Type = type;
            Videos = new List<Video>();
        }

        // Returns the number of Videos in this Library
        public int Length()
        {
            return Videos.Count();
        }

        // Add a video to the library by specifying all relevant information
        public void Add(string name, string genre, string description, float rating)
        {
            Videos.Add(new Video(name, genre, description, rating));
        }

        // Removes a specific video by name
        // Returns the number of videos removed
        public int Remove(string name)
        {
            return Videos.RemoveAll(delegate(Video v) { return v.GetName() == name; });
        }

        // Checks to see if a video is in the Library by name
        public bool Exists(string name)
        {
            return Videos.Exists(delegate(Video v) { return v.GetName() == name; });
        }

        // Finds a Video by name and return a copy of it
        public Video GetByName(string name)
        {
            return Videos.Find(delegate(Video v) { return v.GetName() == name; });
        }


    }
}
