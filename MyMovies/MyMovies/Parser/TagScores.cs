using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies.Parser
{
    class TagScores
    {
        public static void ReadAsync()
        {
            var inputFileStrings = new BlockingCollection<string>();
            var task1 = Downloader.LoadContentAsync(@"D:\data\ml-latest (1)\ml-latest\TagScores_MovieLens.csv", inputFileStrings);
            var dict = new ConcurrentDictionary<string, HashSet<Tag>>();
            var task2 = ProcessContentAsync(inputFileStrings, dict, new char[] { ',' });
            task2.Wait();
        }

        public static Task ProcessContentAsync(BlockingCollection<string> input, ConcurrentDictionary<string, HashSet<Tag>> output, char[] spliters)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in input.GetConsumingEnumerable())
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
