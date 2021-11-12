using MyMovies.Parser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies
{
    class ActorDirectorNames: DataParser<string, Actor> // key --- actorID, value --- Actor
    {
        public ConcurrentDictionary<string, HashSet<string>> secondDict = new ConcurrentDictionary<string, HashSet<string>>();
        public ActorDirectorNames(): base(new char[] { '	' }, @"D:\data\ml-latest (1)\ml-latest\ActorsDirectorsNames_IMDB.txt") { }

        protected override Task ParseData()
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in inputFileStrings.GetConsumingEnumerable())
                {
                    string[] words = line.Split(spliters);
                    output.AddOrUpdate(words[0], new Actor(words[1]), ((x, y) => y));

                    string[] films = words[5].Split(','); // ??????????????????????
                    secondDict.AddOrUpdate(words[1], new HashSet<string>(films), (x, y) => y);
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
//words[5].Split(',') - films
