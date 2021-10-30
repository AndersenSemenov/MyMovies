using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies
{
    class Pipeline
    {
        public static void ReadAsync()
        {
            var bc = new BlockingCollection<string>();
            var task1 = Downloader.LoadContentAsync(@"D:\data\ml-latest (1)\ml-latest\MovieCodes_IMDB.tsv", bc);

            var dict = new ConcurrentDictionary<string, string>();
            var task2 = ProcessContentAsync(bc, dict, new char[] { ' ' });
            task2.Wait();

        }

        public static Task ProcessContentAsync(BlockingCollection<string> input, ConcurrentDictionary<string, string> output, char[] spliters)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in input.GetConsumingEnumerable())
                {
                    string[] words = line.Split(spliters);
                    if (words[4] == "ru" || words[4] == "en")
                    {
                       output.AddOrUpdate(words[0], words[2], ((x, y) => y));
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
