namespace MyMovies
{
    public class Tag
    {
        public string Value { get; private set; } 
        public Tag(string value)
        {
            Value = value;
        }
    }
}