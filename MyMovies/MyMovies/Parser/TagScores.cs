using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies.Parser
{
    class TagScores: DataParser<string, HashSet<int>> // key --- movieID, value --- set of tag_Ids for the movie
    {
        public TagScores(): base(new char[] { ',' }, @"D:\data\ml-latest (1)\ml-latest\TagScores_MovieLens.csv") { }

        protected override Task ParseData()
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in inputFileStrings.GetConsumingEnumerable())
                {
                    string[] words = line.Split(spliters);
                    var a = Convert.ToDouble(words[2].Replace('.', ','));
                    if (Convert.ToDouble(words[2].Replace('.', ',')) >= 0.5)
                    {
                        output.AddOrUpdate(words[0],
                            new HashSet<int>(Convert.ToInt32(words[1])),
                            (x, y) =>
                            {
                                y.Add(Convert.ToInt32(words[1]));
                                return y;
                            });
                        
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
