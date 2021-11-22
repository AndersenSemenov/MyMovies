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
        public Ratings(): base('\t', @"D:\data\ml-latest (1)\ml-latest\Ratings_IMDB.tsv") { }

        protected override void ParseData()
        {
            foreach (var line in inputFileStrings.GetConsumingEnumerable())
            {
                string[] words = line.Split(spliter);
                if (words[0] != "tconst")
                {
                    dict.AddOrUpdate(words[0], Convert.ToDouble(words[1].Replace('.', ',')), ((x, y) => y));
                }
            }
        }
    }
}
