using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies
{
    class ActorDirectorCodes
    {
        public static void ReadAsync()
        {
            var inputFileStrings = new BlockingCollection<string>();
            var task1 = Downloader.LoadContentAsync(@"D:\data\ml-latest (1)\ml-latest\ActorsDirectorsCodes_IMDB.tsv", inputFileStrings);
            var namesCodesDict = new ConcurrentDictionary<string, HashSet<Actor>>();
            var task2 = ProcessContentAsync(inputFileStrings, namesCodesDict, new char[] { '	' });
            task2.Wait();
        }

        public static Task ProcessContentAsync(BlockingCollection<string> input, ConcurrentDictionary<string, HashSet<Actor>> output, char[] spliters)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in input.GetConsumingEnumerable())
                {
                    string[] words = line.Split(spliters);
                    output.AddOrUpdate(words[0], 
                        new HashSet<Actor>(new Actor[] { new Actor($"{words[2]}") }),
                        (x, y) =>
                        {
                            y.Add(new Actor($"{words[2]}"));
                            return y;
                        });
                
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
