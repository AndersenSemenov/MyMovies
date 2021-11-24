using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyMovies
{
    class Movie
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public HashSet<Person> Actors { get; set; }
        public Person Director { get; set; }
        public HashSet<Tag> Tags { get; set; }

        public Movie() { }

        public Movie(string id, string name, double rating, HashSet<Person> actors, Person director, HashSet<Tag> tags)
        {
            Id = id;
            Name = name;
            Rating = rating;
            Actors = actors;
            Director = director;
            Tags = tags;
        }
    }
}
