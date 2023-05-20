using AnimeReviewsData.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeReviewsData.Data
{
    public class AnimeReviewDbContext : DbContext
    {
        public AnimeReviewDbContext(DbContextOptions<AnimeReviewDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Anime> Anime { get; set; }
        public DbSet<AnimeAuthor> AnimeAuthors { get; set; }
        public DbSet<AnimeCategory> AnimeCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AnimeCategory>()
                .HasKey(ac => new { ac.AnimeId, ac.CategoryId });
            builder.Entity<AnimeCategory>()
                .HasOne(a => a.Anime)
                .WithMany(ac => ac.AnimeCategories)
                .HasForeignKey(c => c.AnimeId);
            builder.Entity<AnimeCategory>()
                .HasOne(a => a.Category)
                .WithMany(ac => ac.AnimeCategories)
                .HasForeignKey(c => c.CategoryId);

            builder.Entity<AnimeAuthor>()
                .HasKey(au => new { au.AnimeId, au.AuthorId });
            builder.Entity<AnimeAuthor>()
                .HasOne(a => a.Anime)
                .WithMany(ac => ac.AnimeAuthors)
                .HasForeignKey(c => c.AnimeId);
            builder.Entity<AnimeAuthor>()
                .HasOne(a => a.Author)
                .WithMany(ac => ac.AnimeAuthors)
                .HasForeignKey(c => c.AuthorId);
        }

        public class AnimeReviewDbContextFactory : IDesignTimeDbContextFactory<AnimeReviewDbContext>
        {
            public AnimeReviewDbContext CreateDbContext(string[] args)
            {
                // Build Config
                IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                // Get connection string
                var optionalBuilder = new DbContextOptionsBuilder<AnimeReviewDbContext>();
                var connectionString = config.GetConnectionString("AnimeReviewsDbConnection");
                optionalBuilder.UseSqlServer(connectionString);
                return new AnimeReviewDbContext(optionalBuilder.Options);
            }
        }
    }
}
