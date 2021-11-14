using MyMovies.Parser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyMovies
{
    class ActorDirectorNames: DataParser<string, Actor> // key --- actorID, value --- Actor
    {
        public ConcurrentDictionary<string, HashSet<string>> secondDict = new ConcurrentDictionary<string, HashSet<string>>();
        public ActorDirectorNames(): base('\t', @"D:\data\ml-latest (1)\ml-latest\ActorsDirectorsNames_IMDB.txt") { }

        protected override void ParseData()
        {
            foreach (var line in inputFileStrings.GetConsumingEnumerable())
            {
                string[] words = line.Split(spliter);
                dict.AddOrUpdate(words[0], new Actor(words[1]), ((x, y) => y));

                string[] films = words[5].Split(',');
                secondDict.AddOrUpdate(words[1], new HashSet<string>(films), (x, y) => y);
            }
        }
    }
}
//words[5].Split(',') - films
