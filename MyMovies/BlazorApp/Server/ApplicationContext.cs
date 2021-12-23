using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Shared;

namespace BlazorApp.Server
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Movies;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasMany(movie => movie.Actors).WithMany(actor => actor.Movies);
            modelBuilder.Entity<Movie>().HasOne(movie => movie.Director).WithMany(director => director.Movies);
            modelBuilder.Entity<Movie>().HasMany(movie => movie.Tags).WithMany(tag => tag.Movies);
        }
    }
}
