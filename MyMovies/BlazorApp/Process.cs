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
        public static ConcurrentDictionary<Director, HashSet<Movie>> directorDictionary = new ConcurrentDictionary<Director, HashSet<Movie>>();
        public static ConcurrentDictionary<Tag, HashSet<Movie>> thirdDictionary = new ConcurrentDictionary<Tag, HashSet<Movie>>();

        public static void GetDictionaries()
        {
            Task actorDirectorNameTask = Task.Run(() => actorDirectorNames.ReadandGetData());
            Task ratingsTask = Task.Run(() => ratings.ReadandGetData());
            Task tagCodesTask = Task.Run(() => tagCodes.ReadandGetData());

            Task actorDirectorCodesTask = actorDirectorNameTask.ContinueWith(x => actorDirectorCodes.ReadandGetData());
            Task movieLensTask = tagCodesTask.ContinueWith(x => movieLens.ReadandGetData());

            Task tagScoresTask = movieLensTask.ContinueWith(x => Process.tagScores.ReadandGetData());

            Task.WaitAll(actorDirectorCodesTask, ratingsTask, tagScoresTask);

            Task movieCodesTask = Task.Run(() => movieCodes.ReadandGetData());
            movieCodesTask.Wait();

            firstDictionary = movieCodes.dict;

            Task getSecondDictionary = Task.Run(() =>
            {
                foreach (var item in firstDictionary)
                {
                    foreach (var actor in item.Value.Actors)
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

            Task getDirectorDictionary = Task.Run(() =>
            {
                foreach (var item in firstDictionary)
                {
                    if (item.Value.Director != null)
                    {
                        directorDictionary.AddOrUpdate(item.Value.Director, new HashSet<Movie>(new Movie[] { item.Value }),
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
                foreach (var item in firstDictionary)
                {
                    foreach (var tag in item.Value.Tags)
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

            Task.WaitAll(getSecondDictionary, getDirectorDictionary, getThirdDictionary);

            Parallel.ForEach(firstDictionary, item =>
            {
                item.Value.GetTopTen();
            });

            using (var context = new ApplicationContext())
            {
                foreach (var movie in firstDictionary)
                {
                    context.Movies.Add(movie.Value);
                }
                context.SaveChanges();
            }
        }
    }
}