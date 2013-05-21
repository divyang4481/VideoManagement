// Author: Sean Kelley
// Date Modified: 21 May 2013
// Assignment: C490 A1
// File: Imdb.cs
// Purpose: To provide an easy way to interact with imdbapi.org.
// Note: Several exceptions could be raised during this process and are not handled here,
//       so they must be handled by the caller.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO; // for StreamReader
using System.Text.RegularExpressions;

namespace VideoManagement
{
    /// <summary>
    /// Provides basic interface to imdbapi.org
    /// </summary>
    public class Imdb
    {
        private const string BaseRequest = "http://imdbapi.org/?type=json&plot=simple&episode=0&limit=1&yg=1&mt=none&"
                                         + "lang=en-US&offset=&aka=simple&release=simple&business=0&tech=0";

        private string Title { get; set; }
        private ushort Year { get; set; }
        private string RequestUrl;

        // Disallow the use of a default constructor
        private Imdb()
        {
        }

        /// <summary>
        /// Construct an IMDB object based on given title and year.
        /// </summary>
        /// <param name="title">The Title of the given TV Show or Movie</param>
        /// <param name="year">The year in which the video was first released.
        /// For TV Shows, the year in which the first episode of season 1 aired.</param>
        public Imdb(string title, ushort year)
        {
            // Format title for request url
            Title = title.Trim().Replace(' ', '+');
            Year = year;

            // Create and set the request URL
            MakeRequest(out RequestUrl);
        }

        /// <summary>
        /// Constructs a URL to use in a request to imdbapi.org using the BaseRequest, Title, and Year.
        /// </summary>
        /// <param name="reqString">The newly created request URL</param>
        /// <remarks>It is assumed that Title has already been transformed. I.E. Spaces are replaced with '+'</remarks>
        private void MakeRequest(out string reqString)
        {
            reqString = string.Format("{0}&title={1}&year={2}", BaseRequest, Title, Year);
        }

        /// <summary>
        /// This will return a dictionary containing the title, genre, and description for a given movie.
        /// </summary>
        /// <returns>A dictionary with the following keys (hopefully) filled in: 
        /// "title", "genre", "description"</returns>
        /// <remarks>Many exceptions can be thrown from this method, it is upto the caller to handle these.</remarks>
        public Dictionary<string, string> GetJsonFromApi()
        {
            // Make sure that the RequestUrl has been formed
            if (RequestUrl == null)
                throw new ArgumentNullException("null RequestUrl when trying to get JSON data");

            // Make a request to the API
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RequestUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Ensure we didn't 404
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            // Read the response stream into jsonString
            string jsonString = (new StreamReader(response.GetResponseStream())).ReadToEnd();

            // Form our dictionary
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["title"] = GetJsonField(jsonString, "title");
            dict["genre"] = GetJsonField(jsonString, "genres");
            dict["description"] = GetJsonField(jsonString, "plot_simple");

            return dict;
        }

        /// <summary>
        /// Parse a string of json and return the data contained within a specific field of it.
        /// </summary>
        /// <param name="source">The JSON to parse which should only contain one TV Show or Movie.</param>
        /// <param name="field">The field to locate and return the contents of</param>
        /// <returns>The contents of the field</returns>
        /// <remarks>Throws an exception if field could not be found. The Regex used only tested on
        /// the following fields: title, genres, plot_simple.</remarks>
        public static string GetJsonField(string source, string field)
        {
            // Create a regex to capture a value, example input:
            // "genres": ["Drama", "Western"],
            // "title": "Django Unchained",
            // Note that it's not perfect and very unreadable (sorry)
            string matchString = field + @""": \[?\s*(.*?)\s*""\]?,";
            Regex regex = new Regex(matchString);
            Match match = regex.Match(source);

            // Make sure it matched. Hopefully if program flow reaches here,
            // we don't have bad input.
            if (!match.Success)
                throw new Exception("Did not match field: " + field);

            // And finally, return the result after removing quotes from it
            return Regex.Replace(match.Groups[1].Value, @"[""]", "");
        }
    }
}

/* Example json from api for reference (line breaks here for readability, would not actually exist)
[
  {
    "genres": [
      "Drama",
      "Western"
    ],
    "runtime": [
      "141 min",
      "165 min"
    ],
    "language": [
      "English"
    ],
    "title": "Django Unchained",
    "poster": "http:\/\/ia.media-imdb.com\/images\/M\/MV5BNjI4NzkzODM3Nl5BMl5BanBnXkFtZTcwMzk1NTIyOA@@._V1._SY317_CR0,0,214,317_.jpg",
    "imdb_url": "http:\/\/www.imdb.com\/title\/tt1853728\/",
    "filming_locations": "Second Line Stages, New Orleans, Louisiana, USA",
    "imdb_id": "tt1853728",
    "directors": [
      "Quentin Tarantino"
    ],
    "writers": [
      "Quentin Tarantino"
    ],
    "actors": [
      "Leonardo DiCaprio",
      "Jonah Hill",
      "Samuel L. Jackson",
      "Christoph Waltz",
      "Jamie Foxx",
      "Kerry Washington",
      "Zoe Bell",
      "Amber Tamblyn",
      "James Remar",
      "Walton Goggins",
      "Don Johnson",
      "Bruce Dern",
      "Robert Carradine",
      "Franco Nero",
      "James Russo"
    ],
    "plot_simple": "With the help of his mentor, a slave-turned-bounty hunter sets out to rescue his wife from a brutal Mississippi plantation owner.",
    "year": 2012,
    "country": [
      "USA"
    ],
    "type": "M",
    "release_date": 20121225,
    "also_known_as": [
      "Django sin cadenas"
    ]
  }
]
*/