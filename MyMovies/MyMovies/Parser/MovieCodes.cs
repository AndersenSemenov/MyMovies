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

                var index = line.IndexOf("\ten\t");
                if (index != -1)
                {
                    var firstIndex = line.IndexOf(spliter);
                    var secondIndex = line.IndexOf(spliter, firstIndex + 1);
                    var thirdIndex = line.IndexOf(spliter, secondIndex + 2);

                    var IMDB_Id = line.Substring(0, firstIndex);
                    var movieName = line.Substring(secondIndex + 1, thirdIndex - secondIndex - 1);
                    if (Process.tagScores.dict.ContainsKey(IMDB_Id) 
                        && Process.ratings.dict.ContainsKey(IMDB_Id)
                        && Process.actorDirectorCodes.dict.ContainsKey(IMDB_Id))
                    {
                        Director director = Process.actorDirectorCodes.directorDict.ContainsKey(IMDB_Id) 
                            ? Process.actorDirectorCodes.directorDict[IMDB_Id] : null;

                        dict.AddOrUpdate(IMDB_Id,
                            new Movie(movieName,
                            Process.ratings.dict[IMDB_Id],
                            Process.actorDirectorCodes.dict[IMDB_Id],
                            director,
                            Process.tagScores.dict[IMDB_Id]), 
                            (x, y) => y);
                    }
                }
            }
        }
    }
}