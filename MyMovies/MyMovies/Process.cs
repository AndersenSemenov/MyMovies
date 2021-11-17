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

            Task task11 = task1.ContinueWith(x => actorDirectorCodes.ReadandGetData());
            Task task33 = task3.ContinueWith(x => movieLens.ReadandGetData());

            Task task333 = task33.ContinueWith(x => tagScores.ReadandGetData());

            Task.WaitAll(task11, task2, task333);

            Task task7 = Task.Run(() => movieCodes.ReadandGetData());
            task7.Wait();

            Task t8 = Task.Run(() => // ??? 
            {
                actorDirectorNames.secondDict.Select(x => x.Value.Select(y => movieCodes.dict[y]));
            });

        }
    }
}


/// ждать 6 тасок+, обработать movieCodes+, запустить 2 словарь+, запустить 3 словарь (паралелльно со 2)