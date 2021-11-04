using MyMovies.Parser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies
{
    class ActorDirectorCodes: DataParser<string, HashSet<Actor>> // gets set of actors from the id of the movie
    {
        public ActorDirectorCodes(): base(new char[] { '	' }, @"D:\data\ml-latest (1)\ml-latest\ActorsDirectorsCodes_IMDB.tsv") { }

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
