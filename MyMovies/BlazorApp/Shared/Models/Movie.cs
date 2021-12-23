using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;
using System.Text.Json.Serialization;

namespace BlazorApp.Shared
{
    public class Movie
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        //[JsonIgnore]
        public HashSet<Actor> Actors { get; set; }
        //[JsonIgnore]
        public Director Director { get; set; }
        //[JsonIgnore]
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
    }
}
