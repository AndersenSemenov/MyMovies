using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies.Parser
{
    class TagScores: DataParser<string, HashSet<Tag>> // key --- movieID, value --- set of tags for the movie
    {
        public TagScores(): base(new char[] { ',' }, @"D:\data\ml-latest (1)\ml-latest\TagScores_MovieLens.csv") { }

        public override void ReadandGetData()
        {
            var task1 = Downloader.LoadContentAsync(pathToTheData, inputFileStrings);
            var task2 = ParseData();
            task2.Wait();
        }

        public override Task ParseData()
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
                            new HashSet<Tag>(new Tag[] { new Tag(Convert.ToInt32(words[1])) }),
                            (x, y) =>
                            {
                                y.Add(new Tag(Convert.ToInt32(words[1])));
                                return y;
                            });
                        
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
