using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies
{
    class MovieCodes
    {
        public static void ReadAsync()
        {
            var inputFileStrings = new BlockingCollection<string>();
            var task1 = Downloader.LoadContentAsync(@"D:\data\ml-latest (1)\ml-latest\MovieCodes_IMDB.tsv", inputFileStrings);
            //task1.Wait();
            var movieDictionary = new ConcurrentDictionary<string, string>();
            var task2 = ProcessContentAsync(inputFileStrings, movieDictionary, new char[] { '	' });
            task2.Wait();
            //return await movieDictionary;
        }

        public static Task ProcessContentAsync(BlockingCollection<string> input, ConcurrentDictionary<string, string> output, char[] spliters)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in input.GetConsumingEnumerable())
                {
                    string[] words = line.Split(spliters);
                    if (words[4] == "en") //words[4] == "ru" 
                    {
                       output.AddOrUpdate(words[0], words[2], ((x, y) => y));
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
