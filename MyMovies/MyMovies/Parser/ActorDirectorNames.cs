using MyMovies.Parser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies
{
    //REDO --- value is an Actor, is evth ok??
    class ActorDirectorNames: DataParser<string, (string, string[])> // key --- actorID, value --- Actor
    {
        public ActorDirectorNames(): base(new char[] { '	' }, @"D:\data\ml-latest (1)\ml-latest\ActorsDirectorsNames_IMDB.txt") { }

        protected override Task ParseData()
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var line in inputFileStrings.GetConsumingEnumerable())
                {
                    string[] words = line.Split(spliters);
                    output.AddOrUpdate(words[0], (words[1], words[5].Split(',')), ((x, y) => y));
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
