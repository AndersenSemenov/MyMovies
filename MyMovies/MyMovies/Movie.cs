using System;
using System.Collections.Generic;
using System.Text;

namespace MyMovies
{
    class Movie
    {
        public string Name { get; private set; }
        public HashSet<Actor> Actors { get; private set; }
        public Director Director { get; private set; }
        public HashSet<Tag> Tags { get; private set; }
        public int Rate { get; private set; }
    }
}
