using MyMovies.Parser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies
{
    class ActorDirectorCodes: DataParser<string, HashSet<string>> // gets set of actors from the id of the movie
    {
        public ActorDirectorCodes(): base(new char[] { '	' }, @"D:\data\ml-latest (1)\ml-latest\ActorsDirectorsCodes_IMDB.tsv") { }

        protected override Task ParseData()
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in inputFileStrings.GetConsumingEnumerable())
                {
                    string[] words = line.Split(spliters);
                    output.AddOrUpdate(words[0], 
                        new HashSet<string>(new string[] {words[2]}),
                        (x, y) =>
                        {
                            y.Add(words[2]);
                            return y;
                        });
                
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
