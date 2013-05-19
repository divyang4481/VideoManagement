using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoManagement;

namespace VideoTests
{
    [TestClass]
    public class ImdbTests
    {
        const string title = "Django Unchained";
        const ushort year = 2012;

        [TestMethod]
        public void GetJsonFieldParsesCorrectly()
        {
            string sample = @"[{""genres"": [""Drama"", ""Western""], ""runtime"": [""141 min"", ""165 min""], ""language"": [""English""], ""title"": ""Django Unchained"", ""poster"": ""http://ia.media-imdb.com/images/M/MV5BNjI4NzkzODM3Nl5BMl5BanBnXkFtZTcwMzk1NTIyOA@@._V1._SY317_CR0,0,214,317_.jpg"", ""imdb_url"": ""http://www.imdb.com/title/tt1853728/"", ""filming_locations"": ""Second Line Stages, New Orleans, Louisiana, USA"", ""imdb_id"": ""tt1853728"", ""directors"": [""Quentin Tarantino""], ""writers"": [""Quentin Tarantino""], ""actors"": [""Leonardo DiCaprio"", ""Jonah Hill"", ""Samuel L. Jackson"", ""Christoph Waltz"", ""Jamie Foxx"", ""Kerry Washington"", ""Zoe Bell"", ""Amber Tamblyn"", ""James Remar"", ""Walton Goggins"", ""Don Johnson"", ""Bruce Dern"", ""Robert Carradine"", ""Franco Nero"", ""James Russo""], ""plot_simple"": ""With the help of his mentor, a slave-turned-bounty hunter sets out to rescue his wife from a brutal Mississippi plantation owner."", ""year"": 2012, ""country"": [""USA""], ""type"": ""M"", ""release_date"": 20121225, ""also_known_as"": [""Django sin cadenas""]}]";
            
            Assert.AreEqual("English", Imdb.GetJsonField(sample, "language"));
            Assert.AreEqual("Drama", Imdb.GetJsonField(sample, "genres"));
            Assert.AreEqual("Django Unchained", Imdb.GetJsonField(sample, "title"));
        }

        [TestMethod]
        public void GetJsonDict()
        {
            //Dictionary<string,string> dict = (new Imdb(title, year)).GetJsonFromApi();
            //Assert.AreEqual("Django Unchained", dict["title"]);
        }
    }
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

            // asserts
            Assert.AreEqual((Videos.GetByName(name)).Title, name, "Video found does not match in name");
            Assert.IsTrue(Videos.GetByName("doesn't exist") == null, "GetByName found something that it shouldn't have");
        }
    }
}
