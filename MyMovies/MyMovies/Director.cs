namespace MyMovies
{
    public class Director
    {
        public string Name { get; private set; }

        public Director(string name)
        {
            this.Name = name;
        }
    }
}