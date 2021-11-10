using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMovies.Parser;

namespace MyMovies
{
    class Classsascs
    {
        public static void GetFirstD()
        {
            var actorDirectorCodes = new ActorDirectorCodes();
            var actorDirectorNames = new ActorDirectorNames();
            var movieLens = new MovieLens();
            var ratings = new Ratings();
            var tagCodes = new TagCodes();
            var tagScores = new TagScores();

            Task task1 = actorDirectorCodes.ReadandGetData();
            Task task2 = actorDirectorNames.ReadandGetData();
            Task task3 = movieLens.ReadandGetData();
            Task task4 = ratings.ReadandGetData();
            Task task5 = tagCodes.ReadandGetData();
            Task task6 = tagScores.ReadandGetData();


            Task t = Task.WhenAll(
                new Task[] {task1, task2, task3, task4, task5, task6});

            var movieCodes = new MovieCodes();
            Task task7 = t.ContinueWith(x => movieCodes.ReadandGetData());
        }
    }
}


/// ждать 6 тасок, обработать movieCodes, запустить 2 словарь, запустить 3 словарь (паралелльно со 2)