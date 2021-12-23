using BlazorApp.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Server
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationContext _context;

        public MovieRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var actor = _context.Actors.Find("nm0000001");

            await _context.Movies.Include(movie => movie.Actors).
            Where(movie => movie.Actors.Contains(actor)).ToListAsync();
            return null;
        }
            
    }
}
