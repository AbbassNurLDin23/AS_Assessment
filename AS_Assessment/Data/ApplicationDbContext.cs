using AS_Assessment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AS_Assessment.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // important!

            // Configure the one-to-many relationship between Category and Recipe
            // When a Category is deleted, its associated Recipes will also be deleted (Cascade Delete)
            builder.Entity<InventoryItem>()
                .HasOne(r => r.Category)
                .WithMany(c => c.InventoryItems)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the one-to-many relationship between ApplicationUser and Recipe
            // When a User is deleted, their associated Recipes will also be deleted (Cascade Delete)
            builder.Entity<InventoryItem>()
                .HasOne(r => r.User)
                .WithMany(u => u.InventoryItems)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the Category entity
            builder.Entity<Category>()
                .HasKey(c => c.Id); // Assuming Name is the primary key for Category

            // Configure the Recipe entity
            builder.Entity<InventoryItem>()
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
