using Microsoft.EntityFrameworkCore;
using Core.Entities;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Context
{

    public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        protected string Schema { get; set; } = "StyleUp";
        protected string TablePrefix { get; set; } = "Entity_";

        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=StyleUp;Username=postgres;Password=23082003");
            }
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CustomCanvas> CustomCanvases { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<ProviderRate> ProviderRates { get; set; }
        public DbSet<TemplateCanvas> TemplateCanvases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
