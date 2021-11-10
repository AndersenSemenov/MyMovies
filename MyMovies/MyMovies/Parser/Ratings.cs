using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies.Parser
{
    class Ratings: DataParser<string, double> /// key --- imdbID, value --- rating
    {
        public Ratings(): base(new char[] { '	' }, @"D:\data\ml-latest (1)\ml-latest\Ratings_IMDB.tsv") { }

        protected override Task ParseData()
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in inputFileStrings.GetConsumingEnumerable())
                {
                    string[] words = line.Split(spliters);
                    output.AddOrUpdate(words[0], Convert.ToDouble(words[1].Replace('.', ',')), ((x, y) => y));
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
