using BlogNET8.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace BlogNET8.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Acá deben ir todos los modelos
        public DbSet<Category> Category {  get; set; }
		public DbSet<Article> Article { get; set; }
		public DbSet<Slider> Slider { get; set; }
		public DbSet<ApplicationUser> ApplicationUser { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Configuración adicional, si es necesario
			modelBuilder.Entity<Article>()
				.HasOne(a => a.Category)
				.WithMany()
				.HasForeignKey(a => a.CategoryId)
				.OnDelete(DeleteBehavior.Restrict); // O cualquier comportamiento que desees
		}
	}
}
