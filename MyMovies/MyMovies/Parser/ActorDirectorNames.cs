using MyMovies.Parser;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyMovies
{
    class ActorDirectorNames: DataParser<string, Person> // key --- personId, value --- Person
    {
        public ActorDirectorNames(): base('\t', @"D:\data\ml-latest (1)\ml-latest\ActorsDirectorsNames_IMDB.txt") { }

        protected override void ParseData()
        {
            Regex regex = new Regex("actor|director");
            foreach (var line in inputFileStrings.GetConsumingEnumerable())
            {
                if (regex.IsMatch(line))
                {
                    var firstIndex = line.IndexOf(spliter);
                    var secondIndex = line.IndexOf(spliter, firstIndex + 1);

                    var personId = line.Substring(0, firstIndex);
                    var person = new Person(line.Substring(firstIndex + 1, secondIndex - firstIndex - 1)); 

                    dict.AddOrUpdate(personId, person, ((x, y) => y));
                }
            }
        }
    }
}