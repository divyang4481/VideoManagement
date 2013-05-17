using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoManagement;

namespace VideoTests
{
    [TestClass]
    public class LibraryTests
    {
        const string name = "Some Name", genre = "Some Genre", description = "Some Description";
        const float rating = 9.1f;

        [TestMethod]
        public void AddVideosToLibrary()
        {
            // setup
            Library Videos = new Library(TypeOfVideo.TVShow);
            Videos.Add(name, genre, description, rating);

            // asserts
            Assert.AreEqual(Videos.Length(), 1, "Library size did not increment");
            Assert.IsTrue(Videos.Exists(name), "Video does not exist after adding");
            Assert.IsFalse(Videos.Exists("doesn't exist"), "Video exists even though it shouldn't");
        }

        [TestMethod]
        public void RemoveVideosFromLibrary()
        {
            //setup
            Library Videos = new Library(TypeOfVideo.TVShow);
            Videos.Add(name, genre, description, rating);

            // asserts
            Assert.AreEqual(Videos.Remove(name), 1, "Number of videos removed does not match expected.");
            Assert.AreEqual(Videos.Length(), 0, "Size of library did not decrease after removal");
        }

        [TestMethod]
        public void GetVideoByNameFromLibrary()
        {
            //setup
            Library Videos = new Library(TypeOfVideo.TVShow);
            Videos.Add(name, genre, description, rating);

            Assert.AreEqual((Videos.GetByName(name)).GetName(), name, "Video found does not match in name");
            Assert.IsTrue(Videos.GetByName("doesn't exist") == null, "GetByName found something that it shouldn't have");
        }
    }
}
