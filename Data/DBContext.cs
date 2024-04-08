using CarShops.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarShops.Data
{
    public class DBContext : IdentityDbContext<AppUser>
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }
    }
}
