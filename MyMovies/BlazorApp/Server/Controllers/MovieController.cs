using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _context;

        public MovieController(IMovieRepository context)
        {
            _context = context;
        }
        

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //return new string[] { "valie" };
            var products = await _context.GetMovies();
            return Ok(products);
        }

        // GET: api/<MovieController>
        //[HttpGet("{ValueSearch}")]
        //public string Get(string ValueSearch)
        //{
        //    return _context.Movies.Where(movie => movie.Name.ToLower() == ValueSearch.ToLower()).Select(movie => movie.Rating).Single().ToString();
        //}

        // GET api/<MovieController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<MovieController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<MovieController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
