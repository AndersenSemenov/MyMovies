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
        public static ActorDirectorCodes actorDirectorCodes = new ActorDirectorCodes();
        public static ActorDirectorNames actorDirectorNames = new ActorDirectorNames();
        public static MovieLens movieLens = new MovieLens();
        public static Ratings ratings = new Ratings();
        public static TagCodes tagCodes = new TagCodes();
        public static TagScores tagScores = new TagScores();
        public static MovieCodes movieCodes = new MovieCodes();

        public static void GetDictionaries()
        {
            Task task1 = Task.Run(() => actorDirectorNames.ReadandGetData());
            Task task2 = Task.Run(() => ratings.ReadandGetData());
            Task task3 = Task.Run(() => tagCodes.ReadandGetData());
            task1.Wait();
            task3.Wait();

            Task task11 = Task.Run(() => actorDirectorCodes.ReadandGetData());
            Task task33 = Task.Run(() => movieLens.ReadandGetData()); //????????
            task33.Wait();

            Task task333 = Task.Run(() => tagScores.ReadandGetData());

            bool aa= true;
            bool ca = false;
            int aaa = 5;

            Task.WaitAll(task11, task2, task333);

            bool a = true;
            bool c = false;
            int aaaa = 5;
            //Task task7 = movieCodes.ReadandGetData();
            //task7.Wait();


            //Task t8 = Task.Run(() =>
            //{
            //    actorDirectorNames.secondDict.Select(x => x.Value.Select(y => movieCodes.dict[y]));
            //});
            
        }

        //public static Movie Method(string IMDB_Id, string movieName)
        //{
        //    double rating = ratings.dict.ContainsKey(IMDB_Id) ? ratings.dict[IMDB_Id] : 0;
        //    HashSet<Actor> actors = new HashSet<Actor>();
        //    if (actorDirectorCodes.dict.ContainsKey(IMDB_Id))
        //    {
        //        actors = 
        //            actorDirectorCodes.dict[IMDB_Id].Select(x => actorDirectorNames.dict.ContainsKey(x) ? actorDirectorNames.dict[x] : null).ToHashSet<Actor>(); //овнокод
        //    }
        //    HashSet<Tag> tags = new HashSet<Tag>();
        //    if (movieLens.dict.ContainsKey(IMDB_Id) && tagScores.dict.ContainsKey(movieLens.dict[IMDB_Id]))
        //    {
        //        tags = tagScores.dict[movieLens.dict[IMDB_Id]].Select(x => tagCodes.dict[x]).ToHashSet<Tag>();
        //    }
        //    return new Movie(movieName, rating, actors, tags);
        //}
    }
}


/// ждать 6 тасок+, обработать movieCodes+, запустить 2 словарь+, запустить 3 словарь (паралелльно со 2)