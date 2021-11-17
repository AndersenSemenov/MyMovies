using System;
using System.Collections.Concurrent;
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

        public static ConcurrentDictionary<string, Movie> firstDictionary = new ConcurrentDictionary<string, Movie>();
        public static ConcurrentDictionary<Actor, HashSet<Movie>> secondDictionary = new ConcurrentDictionary<Actor, HashSet<Movie>>();
        public static ConcurrentDictionary<Tag, HashSet<Movie>> thirdDictionary = new ConcurrentDictionary<Tag, HashSet<Movie>>();

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

            Task getFirstDictionary = Task.Run(() => // 1 словарь 
            {
                Parallel.ForEach(movieCodes.dict, item =>
                {
                    firstDictionary.AddOrUpdate(item.Value.Name, item.Value, (x, y) => y);
                });
            });
            getFirstDictionary.Wait();

            Task getSecondDictionary = Task.Run(() =>
            {
                foreach(var item in firstDictionary)
                {
                    foreach(var actor in item.Value.Actors)
                    {
                        secondDictionary.AddOrUpdate(actor, new HashSet<Movie>(new Movie[] { item.Value }),
                            (x, y) =>
                            {
                                y.Add(item.Value);
                                return y;
                            });
                    }
                }
            });

            Task getThirdDictionary = Task.Run(() =>
            {
                foreach(var item in firstDictionary)
                {
                    foreach(var tag in item.Value.Tags)
                    {
                        thirdDictionary.AddOrUpdate(tag, new HashSet<Movie>(new Movie[] { item.Value }),
                            (x, y) =>
                            {
                                y.Add(item.Value);
                                return y;
                            });
                    }
                }
            });

            Task.WaitAll(getSecondDictionary, getThirdDictionary);
        }
    }
}


/// ждать 6 тасок+, обработать movieCodes+, запустить 2 словарь+, запустить 3 словарь (паралелльно со 2)