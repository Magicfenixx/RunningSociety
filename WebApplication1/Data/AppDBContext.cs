using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AppDBContext: IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options ) : base(options)
        {
            
        }
        public DbSet<Race> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }

        public DbSet<Adress> Adresses { get; set; }

    }
}
