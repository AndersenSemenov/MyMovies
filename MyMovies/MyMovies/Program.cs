using MyMovies.Parser;
using System;

namespace MyMovies
{
    class Program
    {
        static void Main(string[] args)
        {
            ActorDirectorNames a = new ActorDirectorNames();
            a.ReadandGetData().Wait();
            //Process.GetDictionaries();
        }
    }
}
