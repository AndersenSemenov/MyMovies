using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies
{
    public class Person
    {
        [Key]
        public string Name { get; set; }

        //public HashSet<Movie> actorMovies { get; set; }

        public Person() { }

        public Person(string name)
            => Name = name;
    }
}
