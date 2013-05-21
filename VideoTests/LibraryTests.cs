// Author: Sean Kelley
// Date Modified: 21 May 2013
// Assignment: C490 A1
// File: LibraryTests.cs
// Purpose: To provide unit testing for classes and methods contained 
//          within the namespace of VideoManagement.
// Notes: In its current state, there is relatively poor coverage.
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoManagement;

namespace VideoTests
{
    /// <summary>
    /// Tests relating to Imdb.cs
    /// </summary>
    [TestClass]
    public class ImdbTests
    {
        const string title = "Django Unchained";
        const ushort year = 2012;

        /// <summary>
        /// Test Imdb.GetJsonField to ensure it correctly parses JSON and returns appropriate values.
        /// </summary>
        [TestMethod]
        public void GetJsonFieldParsesCorrectly()
        {
            // A sample JSON string to use for testing
            string sample = @"[{""genres"": [""Drama"", ""Western""], ""runtime"": [""141 min"", ""165 min""], ""language"": [""English""], ""title"": ""Django Unchained"", ""poster"": ""http://ia.media-imdb.com/images/M/MV5BNjI4NzkzODM3Nl5BMl5BanBnXkFtZTcwMzk1NTIyOA@@._V1._SY317_CR0,0,214,317_.jpg"", ""imdb_url"": ""http://www.imdb.com/title/tt1853728/"", ""filming_locations"": ""Second Line Stages, New Orleans, Louisiana, USA"", ""imdb_id"": ""tt1853728"", ""directors"": [""Quentin Tarantino""], ""writers"": [""Quentin Tarantino""], ""actors"": [""Leonardo DiCaprio"", ""Jonah Hill"", ""Samuel L. Jackson"", ""Christoph Waltz"", ""Jamie Foxx"", ""Kerry Washington"", ""Zoe Bell"", ""Amber Tamblyn"", ""James Remar"", ""Walton Goggins"", ""Don Johnson"", ""Bruce Dern"", ""Robert Carradine"", ""Franco Nero"", ""James Russo""], ""plot_simple"": ""With the help of his mentor, a slave-turned-bounty hunter sets out to rescue his wife from a brutal Mississippi plantation owner."", ""year"": 2012, ""country"": [""USA""], ""type"": ""M"", ""release_date"": 20121225, ""also_known_as"": [""Django sin cadenas""]}]";
            
            Assert.AreEqual("English", Imdb.GetJsonField(sample, "language"));
            Assert.AreEqual("Drama", Imdb.GetJsonField(sample, "genres"));
            Assert.AreEqual("Django Unchained", Imdb.GetJsonField(sample, "title"));
        }
    }

    /// <summary>
    /// Tests relating to Library.cs
    /// </summary>
    [TestClass]
    public class LibraryTests
    {
        const string name = "Some Name", genre = "Some Genre", description = "Some Description";
        const ushort year = 2012;

        /// <summary>
        /// Ensure that a video is properly added to the library when specifying all information.
        /// </summary>
        [TestMethod]
        public void AddVideosToLibrary()
        {
            // setup
            Library Videos = new Library(TypeOfVideo.TVShow);
            Videos.Add(name, genre, description, year);

            // asserts
            Assert.AreEqual(Videos.Length(), 1, "Library size did not increment");
            Assert.IsTrue(Videos.Exists(name), "Video does not exist after adding");
            Assert.IsFalse(Videos.Exists("doesn't exist"), "Video exists even though it shouldn't");
        }

        /// <summary>
        /// Ensure that videos are properly removed from Library.
        /// </summary>
        [TestMethod]
        public void RemoveVideosFromLibrary()
        {
            //setup
            Library Videos = new Library(TypeOfVideo.TVShow);
            Videos.Add(name, genre, description, year);

            // asserts
            Assert.AreEqual(Videos.Remove(name), 1, "Number of videos removed does not match expected.");
            Assert.AreEqual(Videos.Length(), 0, "Size of library did not decrease after removal");
        }

        /// <summary>
        /// Ensure that videos can be retrieved by name.
        /// </summary>
        [TestMethod]
        public void GetVideoByNameFromLibrary()
        {
            //setup
            Library Videos = new Library(TypeOfVideo.TVShow);
            Videos.Add(name, genre, description, year);

            // asserts
            Assert.AreEqual((Videos.GetByName(name)).Title, name, "Video found does not match in name");
            Assert.IsTrue(Videos.GetByName("doesn't exist").Title == null, "GetByName found something that it shouldn't have");
        }
    }
}
