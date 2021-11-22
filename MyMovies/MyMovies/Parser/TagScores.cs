using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies.Parser
{
    class TagScores: DataParser<string, HashSet<Tag>> // key --- movieID, value --- set of tag_Ids for the movie
    {
        public TagScores(): base(',', @"D:\data\ml-latest (1)\ml-latest\TagScores_MovieLens.csv") { }

        protected override void ParseData()
        {
            foreach (var line in inputFileStrings.GetConsumingEnumerable())
            {
                string[] words = line.Split(spliter);
                if (Convert.ToDouble(words[2].Replace('.', ',')) >= 0.5)
                {
                    var key = Process.movieLens.dict[words[0]];
                    var value = Process.tagCodes.dict[Convert.ToInt32(words[1])];
                    dict.AddOrUpdate(key,
                        new HashSet<Tag>(new Tag[] { value }),
                        (x, y) =>
                        {
                            y.Add(value);
                            return y;
                        });

                }
            }
        }
    }
}
