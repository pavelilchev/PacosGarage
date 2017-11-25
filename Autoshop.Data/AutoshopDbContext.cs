namespace Autoshop.Data
{
    using Autoshop.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class AutoshopDbContext : IdentityDbContext<User>
    {
        public AutoshopDbContext(DbContextOptions<AutoshopDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
