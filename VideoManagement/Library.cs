using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoManagement
{
    public class Library
    {
        private List<Video> Videos;
        TypeOfVideo Type;

        public Library(TypeOfVideo type)
        {
            Type = type;
            Videos = new List<Video>();
        }
        
        public int Length()
        {
            return Videos.Count();
        }

        public void Add(string name, string genre, string description, float rating)
        {
            Videos.Add(new Video(name, genre, description, rating));
        }

        // Returns the number of videos removed
        public int Remove(string name)
        {
            return Videos.RemoveAll(delegate(Video v) { return v.GetName() == name; });
        }

        public bool Exists(string name)
        {
            return Videos.Exists(delegate(Video v) { return v.GetName() == name; });
        }

        public Video GetByName(string name)
        {
            return Videos.Find(delegate(Video v) { return v.GetName() == name; });
        }


    }
}
