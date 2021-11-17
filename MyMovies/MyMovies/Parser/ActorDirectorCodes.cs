using MyMovies.Parser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies
{
    class ActorDirectorCodes: DataParser<string, HashSet<Person>> // gets set of actors and director from the id of the movie
    {
        public ConcurrentDictionary<string, Person> directorDict = new ConcurrentDictionary<string, Person>();
        public ActorDirectorCodes(): base('\t', @"D:\data\ml-latest (1)\ml-latest\ActorsDirectorsCodes_IMDB.tsv") { }

        protected override void ParseData()
        {
            foreach (var line in inputFileStrings.GetConsumingEnumerable())
            {

                var firstIndex = line.IndexOf(spliter);
                var secondIndex = line.IndexOf(spliter, firstIndex + 1);
                var thirdIndex = line.IndexOf(spliter, secondIndex + 1);
                var fourthIndex = line.IndexOf(spliter, thirdIndex + 1);

                var category = line.Substring(thirdIndex + 1, fourthIndex - thirdIndex - 1);
                if (category == "actor")
                {
                    var IMDB_Id = line.Substring(0, firstIndex);
                    var actor_Id = line.Substring(secondIndex + 1, thirdIndex - secondIndex - 1);

                    if (Process.actorDirectorNames.dict.ContainsKey(actor_Id))
                    {
                        dict.AddOrUpdate(IMDB_Id,
                        new HashSet<Person>(new Person[] { Process.actorDirectorNames.dict[actor_Id] }),
                        (x, y) =>
                        {
                            y.Add(Process.actorDirectorNames.dict[actor_Id]);
                            return y;
                        });
                    }
                }
                else if (category == "director")
                {
                    var IMDB_Id = line.Substring(0, firstIndex);
                    var director_Id = line.Substring(secondIndex + 1, thirdIndex - secondIndex - 1);

                    if (Process.actorDirectorNames.dict.ContainsKey(director_Id))
                    {
                        directorDict.AddOrUpdate(IMDB_Id, Process.actorDirectorNames.dict[director_Id], (x, y) => y);
                    }
                }
            }
        }
    }
}
