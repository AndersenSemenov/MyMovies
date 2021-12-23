using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Shared;

namespace BlazorApp.Server
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetMovies();
    }
}
