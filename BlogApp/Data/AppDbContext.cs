using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tables
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category → Post = 1 : M
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Post → Comment = 1 : M
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Technology",
                    Description = "Posts related to technology."
                },
                new Category
                {
                    Id = 2,
                    Name = "Lifestyle",
                    Description = "Posts related to lifestyle."
                },
                new Category
                {
                    Id = 3,
                    Name = "Travel",
                    Description = "Posts related to travel."
                }
            );

            // Seed Posts
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Title = "Introduction to ASP.NET Core",
                    Content = "ASP.NET Core is a cross-platform framework for building modern web applications.",
                    Author = "Scilent Knight",
                    FeatureImagePath = "/images/aspnet-core.jpg",
                    PublishedDate = new DateTime(2026, 8, 9),
                    CategoryId = 1
                },
                new Post
                {
                    Id = 2,
                    Title = "10 Tips for a Healthy Lifestyle",
                    Content = "Maintaining a healthy lifestyle is essential for overall well-being. Here are 10 tips to help you stay healthy.",
                    Author = "Hande Ercel",
                    FeatureImagePath = "/images/healthy-lifestyle.jpg",
                    PublishedDate = new DateTime(2026, 8, 9),
                    CategoryId = 2
                },
                new Post
                {
                    Id = 3,
                    Title = "Top Travel Destinations for 2026",
                    Content = "Discover the top travel destinations for 2026 and plan your next adventure.",
                    Author = "Noraly Knight",
                    FeatureImagePath = "/images/travel-destinations.jpg",
                    PublishedDate = new DateTime(2026, 8, 9),
                    CategoryId = 3
                }
            );

            // Seed Comments
            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    Id = 1,
                    Username = "John Doe",
                    CommentDate = new DateTime(2026, 8, 10),
                    Content = "Great introduction to ASP.NET Core! Thanks for sharing.",
                    PostId = 1
                },
                new Comment
                {
                    Id = 2,
                    Username = "Jane Smith",
                    CommentDate = new DateTime(2026, 8, 11),
                    Content = "These tips are really helpful! I'm going to try implementing them.",
                    PostId = 2
                },
                new Comment
                {
                    Id = 3,
                    Username = "Alice Johnson",
                    CommentDate = new DateTime(2026, 8, 12),
                    Content = "I can't wait to visit these travel destinations! Thanks for the recommendations.",
                    PostId = 3
                }
            );
        }
    }
}