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
        public ActorDirectorCodes(): base('\t', @"D:\data\ml-latest (1)\ml-latest\ActorsDirectorsCodes_IMDB.tsv") { }

        protected override void ParseData()
        {
            foreach (var line in inputFileStrings.GetConsumingEnumerable())
            {
                var index = line.IndexOf(spliter);
                var IMDB_Id = line.Substring(0, 9);
                var actor_Id = line.Substring(index + 3, 9);
                dict.AddOrUpdate(IMDB_Id,
                    new HashSet<Actor>(new Actor[] { Process.actorDirectorNames.dict[actor_Id] }),
                    (x, y) =>
                    {
                        y.Add(Process.actorDirectorNames.dict[actor_Id]);
                        return y;
                    });

            }
        }
    }
}
