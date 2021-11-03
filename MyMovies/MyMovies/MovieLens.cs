using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies
{
    class MovieLens
    {
        public static void ReadAsync()
        {
            var inputFileStrings = new BlockingCollection<string>();
            var task1 = Downloader.LoadContentAsync(@"D:\data\ml-latest (1)\ml-latest\links_IMDB_MovieLens.csv", inputFileStrings);
            var dict = new ConcurrentDictionary<string, string>();
            var task2 = ProcessContentAsync(inputFileStrings, dict, new char[] {','});
            task2.Wait();
        }

        public static Task ProcessContentAsync(BlockingCollection<string> input, ConcurrentDictionary<string, string> output, char[] spliters)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in input.GetConsumingEnumerable())
                {
                    string[] words = line.Split(spliters);
                    output.AddOrUpdate(words[0], $"tt{words[1]}", ((x, y) => y));
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
