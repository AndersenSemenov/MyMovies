using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies
{
    class ActorDirectorsNames
    {
        public static void ReadAsync()
        {
            var inputFileStrings = new BlockingCollection<string>();
            var task1 = Downloader.LoadContentAsync(@"D:\data\ml-latest (1)\ml-latest\ActorsDirectorsNames_IMDB.txt", inputFileStrings);
            var namesCodesDict = new ConcurrentDictionary<string, (string, string[])>();
            var task2 = ProcessContentAsync(inputFileStrings, namesCodesDict, new char[] { '	' });// '	'
            task2.Wait();
        }

        public static Task ProcessContentAsync(BlockingCollection<string> input, ConcurrentDictionary<string, (string, string[])> output, char[] spliters)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in input.GetConsumingEnumerable())
                {
                    string[] words = line.Split(spliters);
                    output.AddOrUpdate(words[0], (words[1], words[5].Split(',')), ((x, y) => y));
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
