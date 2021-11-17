using System;
using System.Collections.Generic;
using System.Text;

namespace MyMovies
{
    class Movie
    {
        public string Name { get; private set; }
        public double Rating { get; private set; }
        public HashSet<Person> Actors { get; private set; }
        public Person Director { get; private set; }
        public HashSet<Tag> Tags { get; private set; }

        public Movie(string name, double rating, HashSet<Person> actors, Person director, HashSet<Tag> tags)
        {
            Name = name;
            Rating = rating;
            Actors = actors;
            Director = director;
            Tags = tags;
        }
    }
}
