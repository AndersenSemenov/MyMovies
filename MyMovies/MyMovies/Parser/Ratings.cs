using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies.Parser
{
    class Ratings
    {
        public static void ReadAsync()
        {
            var inputFileStrings = new BlockingCollection<string>();
            var task1 = Downloader.LoadContentAsync(@"D:\data\ml-latest (1)\ml-latest\Ratings_IMDB.tsv", inputFileStrings);
            var dict = new ConcurrentDictionary<string, double>();
            var task2 = ProcessContentAsync(inputFileStrings, dict, new char[] { '	' });
            task2.Wait();
        }

        public static Task ProcessContentAsync(BlockingCollection<string> input, ConcurrentDictionary<string, double> output, char[] spliters)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in input.GetConsumingEnumerable())
                {
                    string[] words = line.Split(spliters);
                    output.AddOrUpdate(words[0], Convert.ToDouble(words[1].Replace('.', ',')), ((x, y) => y));
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
