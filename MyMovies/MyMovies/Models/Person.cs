using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies
{
    public abstract class Person
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public HashSet<Movie> Movies { get; set; }

        public Person() { }

        public Person(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
