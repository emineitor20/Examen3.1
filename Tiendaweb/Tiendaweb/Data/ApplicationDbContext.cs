using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tiendaweb.Models;

namespace Tiendaweb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<estilo> estilo { get; set; }
        public DbSet<cervezas> cervezas { get; set; }
    }
}