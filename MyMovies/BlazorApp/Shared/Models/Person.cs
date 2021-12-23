using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorApp.Shared
{
    public abstract class Person
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        //[JsonIgnore]
        public HashSet<Movie> Movies { get; set; }

        public Person() { }

        public Person(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
