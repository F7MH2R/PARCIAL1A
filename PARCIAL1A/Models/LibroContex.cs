using Microsoft.EntityFrameworkCore;

namespace PARCIAL1A.Models
{
    public class LibroContex:DbContext
    {
        public LibroContex(DbContextOptions<LibroContex> options) : base(options)
        {

        }

    }
}
