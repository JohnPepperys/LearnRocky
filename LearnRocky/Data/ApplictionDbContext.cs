using LearnRocky.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearnRocky.Data
{
    public class ApplictionDbContext : IdentityDbContext
    {
        public ApplictionDbContext(DbContextOptions<ApplictionDbContext> options) : base(options) { 
            Database.EnsureCreated();
        }

        public DbSet<Category> Categories { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<ApplicationUser> appUSer { set; get; }
    }
}
