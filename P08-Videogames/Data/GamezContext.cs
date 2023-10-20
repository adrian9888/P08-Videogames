using Microsoft.EntityFrameworkCore;
using P08_Videogames.Models;

namespace P08_Videogames.Data
{
    public class GamezContext:DbContext
    {
        public GamezContext(DbContextOptions<GamezContext>o)
            : base(o) { }
        public DbSet<Gamez> LosGamez { get; set; }
    }
}
