using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class Tag
    {
        [Key]
        public string Value { get; set; }
        public HashSet<Movie> Movies { get; set; }

        public Tag() { }

        public Tag(string value)
            => Value = value;
    }
}