using MyMovies.Parser;
using System;
using System.Text.RegularExpressions; //?

namespace MyMovies
{
    class Program
    {
        static void Main(string[] args)
        {
            MovieCodes a = new MovieCodes();
            a.ReadandGetData();
            //Process.GetDictionaries();
        }
    }
}
