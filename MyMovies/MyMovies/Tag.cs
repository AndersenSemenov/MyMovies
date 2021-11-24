using System.ComponentModel.DataAnnotations;

namespace MyMovies
{
    public class Tag
    {
        [Key]
        public string Value { get; set; }

        public Tag() { }

        public Tag(string value)
            => Value = value;
    }
}