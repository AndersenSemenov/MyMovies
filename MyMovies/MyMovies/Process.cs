using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMovies.Parser;

namespace MyMovies
{
    static class Process
    {
        private static ActorDirectorCodes actorDirectorCodes = new ActorDirectorCodes();
        private static ActorDirectorNames actorDirectorNames = new ActorDirectorNames();
        private static MovieLens movieLens = new MovieLens();
        private static Ratings ratings = new Ratings();
        private static TagCodes tagCodes = new TagCodes();
        private static TagScores tagScores = new TagScores();
        private static MovieCodes movieCodes = new MovieCodes();

        public static void GetDictionaries()
        {
            Task task1 = actorDirectorCodes.ReadandGetData();
            Task task2 = actorDirectorNames.ReadandGetData();
            Task task3 = movieLens.ReadandGetData();
            Task task4 = ratings.ReadandGetData();
            Task task5 = tagCodes.ReadandGetData();
            Task task6 = tagScores.ReadandGetData();

            Task t = Task.WhenAll(
                new Task[] {task1, task2, task3, task4, task5, task6});

            t.Wait();

            Task task7 = movieCodes.ReadandGetData();//t.ContinueWith(x => movieCodes.ReadandGetData());
            task7.Wait();


            Task t8 = Task.Run(() =>
            {
                actorDirectorNames.secondDict.Select(x => x.Value.Select(y => movieCodes.output[y]));
            });
            
        }

        public static Movie Method(string IMDB_Id, string movieName)
        {
            double rating = ratings.output.ContainsKey(IMDB_Id) ? ratings.output[IMDB_Id] : 0;
            HashSet<Actor> actors = new HashSet<Actor>();
            if (actorDirectorCodes.output.ContainsKey(IMDB_Id))
            {
                actors = 
                    actorDirectorCodes.output[IMDB_Id].Select(x => actorDirectorNames.output.ContainsKey(x) ? actorDirectorNames.output[x] : null).ToHashSet<Actor>(); //овнокод
            }
            HashSet<Tag> tags = new HashSet<Tag>();
            if (movieLens.output.ContainsKey(IMDB_Id) && tagScores.output.ContainsKey(movieLens.output[IMDB_Id]))
            {
                tags = tagScores.output[movieLens.output[IMDB_Id]].Select(x => tagCodes.output[x]).ToHashSet<Tag>();
            }
            return new Movie(movieName, rating, actors, tags);
        }
    }
}


/// ждать 6 тасок+, обработать movieCodes+, запустить 2 словарь+, запустить 3 словарь (паралелльно со 2)