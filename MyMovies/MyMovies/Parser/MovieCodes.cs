using MyMovies.Parser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyMovies
{
    class MovieCodes : DataParser<string, Movie> // key --- IMDB_ID, value --- movie
    {
        public MovieCodes() : base('\t', @"D:\data\ml-latest (1)\ml-latest\MovieCodes_IMDB.tsv") { }

        protected override void ParseData()
        {
            foreach (var line in inputFileStrings.GetConsumingEnumerable())
            {
                Regex regex = new Regex("\ben\b");
                MatchCollection matches = regex.Matches(line);
                if (matches.Count != 0)
                {
                    var firstIndex = line.IndexOf(spliter);
                    var secondIndex = line.IndexOf(spliter, firstIndex + 1);
                    var thirdIndex = line.IndexOf(spliter, secondIndex + 2);

                    var IMDB_Id = line.Substring(0, 9);
                    var movieName = line.Substring(secondIndex + 1, thirdIndex - secondIndex - 1);
                    if (Process.tagScores.dict.ContainsKey(IMDB_Id) 
                        && Process.ratings.dict.ContainsKey(IMDB_Id) 
                        && Process.actorDirectorCodes.dict.ContainsKey(IMDB_Id))
                    {
                        var rating = Process.ratings.dict[IMDB_Id];
                        var actors = Process.actorDirectorCodes.dict[IMDB_Id];
                        var tags = Process.tagScores.dict[IMDB_Id];
                        dict.AddOrUpdate(IMDB_Id, new Movie(movieName, rating, actors, tags), (x, y) => y);
                    }
                }
            }
        }
    }
}

//if (words[4] == "en")
//{
//    var movieName = words[2];
//    var rating = Process.ratings.dict[words[0]];
//    var actors = Process.actorDirectorCodes.dict[words[0]];
//    var tags = Process.tagScores.dict[words[0]];
//    dict.AddOrUpdate(words[0], new Movie(movieName, rating, actors, tags), (x, y) => y);
//}