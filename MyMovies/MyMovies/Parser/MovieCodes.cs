using MyMovies.Parser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies
{
    class MovieCodes: DataParser<string, string> // key --- IMDB_ID, value --- movieName
    {
        public MovieCodes(): base(new char[] { '	' }, @"D:\data\ml-latest (1)\ml-latest\MovieCodes_IMDB.tsv") { }

        protected override Task ParseData()
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in inputFileStrings.GetConsumingEnumerable())
                {
                    string[] words = line.Split(spliters);
                    if (words[4] == "en") //words[4] == "ru" 
                    {
                       output.AddOrUpdate(words[0], words[2], ((x, y) => y));
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
