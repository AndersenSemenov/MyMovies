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
    class MovieCodes : DataParser<string, Movie> // key --- movieName, value --- movie
    {
        public MovieCodes() : base('\t', @"D:\data\ml-latest (1)\ml-latest\MovieCodes_IMDB.tsv") { }

        protected override void ParseData()
        {
            Regex regex = new Regex("\ten|ru\t");
            Regex regexEn = new Regex("\ten\t");

            foreach (var line in inputFileStrings.GetConsumingEnumerable())
            {  
                if (regex.IsMatch(line))
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
                        var director = Process.actorDirectorCodes.directorDict.ContainsKey(IMDB_Id) 
                            ? Process.actorDirectorCodes.directorDict[IMDB_Id] : null;

                        if (!dict.ContainsKey(IMDB_Id) ||
                             dict.ContainsKey(IMDB_Id) && movieName != dict[IMDB_Id].Name)
                        {
                            
                            dict.AddOrUpdate(IMDB_Id + (regexEn.IsMatch(line) ? "en" : "ru"),
                            new Movie
                            (IMDB_Id + (regexEn.IsMatch(line) ? "en" : "ru"),
                            movieName,
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
}