using Microsoft.EntityFrameworkCore;

namespace PARCIAL1A.Models
{
    public class LibroContex:DbContext
    {
        public LibroContex(DbContextOptions<LibroContex> options) : base(options)
        {

        }
        public DbSet<Libros> Libros { get; set; }
        public DbSet<Autores> Autores { get; set; }
        public DbSet<AutorLibro> AutorLibro { get; set;}
        public DbSet<Post> Posts { get; set; }
    }
}
