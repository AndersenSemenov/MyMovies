using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyMovies
{
    class Movie
    {
        [Key]
        public string Id { get; private set; }
        public string Name { get; private set; }
        public double Rating { get; private set; }
        public HashSet<Person> Actors { get; private set; }
        public Person Director { get; private set; }
        public HashSet<Tag> Tags { get; private set; }

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
