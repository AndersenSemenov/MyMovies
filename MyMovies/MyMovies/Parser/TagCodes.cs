using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies.Parser
{
    public class TagCodes
    {
        public static void ReadAsync()
        {
            var inputFileStrings = new BlockingCollection<string>();
            var task1 = Downloader.LoadContentAsync(@"D:\data\ml-latest (1)\ml-latest\TagCodes_MovieLens.csv", inputFileStrings);
            var dict = new ConcurrentDictionary<int, string>();
            var task2 = ProcessContentAsync(inputFileStrings, dict, new char[] {','});
            task2.Wait();
        }

        public static Task ProcessContentAsync(BlockingCollection<string> input, ConcurrentDictionary<int, string> output, char[] spliters)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in input.GetConsumingEnumerable())
                {
                    string[] words = line.Split(spliters);
                    output.AddOrUpdate(Convert.ToInt32(words[0]), words[1], ((x, y) => y));
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
