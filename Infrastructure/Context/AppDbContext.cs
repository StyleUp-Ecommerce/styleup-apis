using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{

    public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        //protected string Schema { get; set; } = "StyleUp";
        //protected string TablePrefix { get; set; } = "Entity_";
        public DbSet<SuggestionCanvas> SuggestionCanvas { get; set; }
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
