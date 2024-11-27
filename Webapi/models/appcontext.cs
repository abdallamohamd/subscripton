using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Webapi.models
{
    public class appcontext : IdentityDbContext<appuser>
    {
        public DbSet<plan> plans { get; set; }
        public DbSet<subscription> subscriptions { get; set; }
        public appcontext(DbContextOptions<appcontext> options) : base(options) { }
        public appcontext() :base( ){ }
    }
}
