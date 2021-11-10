using MyMovies.Parser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies
{
    class MovieCodes : DataParser<string, Movie> // key --- IMDB_ID, value --- movieName
    {
        public MovieCodes() : base(new char[] { '	' }, @"D:\data\ml-latest (1)\ml-latest\MovieCodes_IMDB.tsv") { }

        protected override Task ParseData()
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in inputFileStrings.GetConsumingEnumerable())
                {
                    string[] words = line.Split(spliters);
                    if (words[4] == "en")
                    {
                        var movie = Process.Method(words[0], words[2]);
                        output.AddOrUpdate(words[2], movie, ((x, y) => y));
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }

        //public Task ParseData(ActorDirectorCodes actorDirectorCodes, ActorDirectorNames actorDirectorNames, )
        //{

        //}
    }
}
