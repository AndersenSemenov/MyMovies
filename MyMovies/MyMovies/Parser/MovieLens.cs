using MyMovies.Parser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies
{
    class MovieLens: DataParser<string, string> // key --- movieID, value --- imdID
    {
        public MovieLens(): base(new char[] {','}, @"D:\data\ml-latest (1)\ml-latest\links_IMDB_MovieLens.csv") { }

        protected override Task ParseData()
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in inputFileStrings.GetConsumingEnumerable())
                {
                    string[] words = line.Split(spliters);
                    output.AddOrUpdate(words[0], $"tt{words[1]}", ((x, y) => y));
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
