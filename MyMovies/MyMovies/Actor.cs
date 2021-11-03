namespace MyMovies
{
    public class Actor
    {
        public string Name { get; private set; }

        public Actor(string Name)
        {
            this.Name = Name;
        }
    }
}