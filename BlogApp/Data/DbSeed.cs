using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data
{
    public static class DBSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Make sure the database is created and migrations are applied
            await context.Database.MigrateAsync();

            // Seed Categories
            if (!await context.Categories.AnyAsync())
            {
                var categories = new List<Category>
                {
                    new Category
                    {
                        Name = "Technology",
                        Description = "Articles about technology and software development."
                    },
                    new Category
                    {
                        Name = "Programming",
                        Description = "Programming tutorials, tips, and best practices."
                    },
                    new Category
                    {
                        Name = "Web Development",
                        Description = "Articles about web development and modern web technologies."
                    }
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            // Seed Posts
            if (!await context.Posts.AnyAsync())
            {
                var technology = await context.Categories
                    .FirstAsync(c => c.Name == "Technology");

                var programming = await context.Categories
                    .FirstAsync(c => c.Name == "Programming");

                var webDevelopment = await context.Categories
                    .FirstAsync(c => c.Name == "Web Development");

                var posts = new List<Post>
                {
                    new Post
                    {
                        Title = "Introduction to ASP.NET Core",
                        Content = "ASP.NET Core is a cross-platform framework for building modern web applications.",
                        Author = "Admin",
                        FeatureImagePath = "/images/aspnet-core.jpg",
                        PublishedDate = DateTime.UtcNow.AddDays(-5),
                        CategoryId = technology.Id
                    },

                    new Post
                    {
                        Title = "Getting Started with C#",
                        Content = "C# is a modern, object-oriented programming language developed by Microsoft.",
                        Author = "Admin",
                        FeatureImagePath = "/images/csharp.jpg",
                        PublishedDate = DateTime.UtcNow.AddDays(-3),
                        CategoryId = programming.Id
                    },

                    new Post
                    {
                        Title = "Building Web Applications with MVC",
                        Content = "ASP.NET Core MVC provides a powerful pattern for building maintainable web applications.",
                        Author = "Admin",
                        FeatureImagePath = "/images/mvc.jpg",
                        PublishedDate = DateTime.UtcNow.AddDays(-1),
                        CategoryId = webDevelopment.Id
                    }
                };

                await context.Posts.AddRangeAsync(posts);
                await context.SaveChangesAsync();
            }

            // Seed Comments
            if (!await context.Comments.AnyAsync())
            {
                var firstPost = await context.Posts
                    .FirstAsync(p => p.Title == "Introduction to ASP.NET Core");

                var secondPost = await context.Posts
                    .FirstAsync(p => p.Title == "Getting Started with C#");

                var comments = new List<Comment>
                {
                    new Comment
                    {
                        Username = "John",
                        Content = "Great introduction to ASP.NET Core!",
                        CommentDate = DateTime.UtcNow.AddDays(-4),
                        PostId = firstPost.Id
                    },

                    new Comment
                    {
                        Username = "Sarah",
                        Content = "This was very helpful for getting started.",
                        CommentDate = DateTime.UtcNow.AddDays(-2),
                        PostId = firstPost.Id
                    },

                    new Comment
                    {
                        Username = "David",
                        Content = "C# is a great language. Thanks for the article!",
                        CommentDate = DateTime.UtcNow.AddDays(-1),
                        PostId = secondPost.Id
                    }
                };

                await context.Comments.AddRangeAsync(comments);
                await context.SaveChangesAsync();
            }
        }
    }
}