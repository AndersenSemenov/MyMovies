using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;

namespace MyMovies
{
    public class Movie
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public HashSet<Actor> Actors { get; set; }
        public Director Director { get; set; }
        public HashSet<Tag> Tags { get; set; }
        public string TopTen { get; set; }

        public Movie() { }

        public Movie(string id, string name, double rating, HashSet<Actor> actors, Director director, HashSet<Tag> tags)
        {
            Id = id;
            Name = name;
            Rating = rating;
            Actors = actors;
            Director = director;
            Tags = tags;
        }

        public void GetTopTen()
        {
            Dictionary<string, double> similarFilms = new Dictionary<string, double>();
            foreach(var item in Process.firstDictionary)
            {
                similarFilms.Add(item.Key, MethodCompare(item.Value));               
            }
            var k = similarFilms.OrderByDescending(item => item.Value).Take(10).Select(item => item.Key + " ").ToList<string>();
            foreach (var str in k)
            {
                TopTen += str;
            }
            int a = 5;
        }

        double MethodCompare(Movie movie)
        {
            var similarK = 0.0;
            if (this.Director == movie.Director)
            {
                similarK += 0.5;
            }
            
            var tagIntersectionSize = 0;
            foreach (var tag in this.Tags)
            {
                tagIntersectionSize += Convert.ToInt32(movie.Tags.Contains(tag));
            }
            similarK += 0.1 * tagIntersectionSize;

            var actorIntersectionSize = 0;
            foreach (var actor in this.Actors)
            {
                actorIntersectionSize += Convert.ToInt32(movie.Actors.Contains(actor));
            }
            similarK += actorIntersectionSize * 0.2;

            return similarK;
        }
        
        
        
        
        // енайти фильм по актеру, по директору, по тегу, по имени фильма, топ 10 похожих
    }
}
