using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedingInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Posts related to technology.", "Technology" },
                    { 2, "Posts related to lifestyle.", "Lifestyle" },
                    { 3, "Posts related to travel.", "Travel" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "FeatureImagePath", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 1, "Scilent Knight", 1, "ASP.NET Core is a cross-platform framework for building modern web applications.", "/images/aspnet-core.jpg", new DateTime(2026, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Introduction to ASP.NET Core" },
                    { 2, "Hande Ercel", 2, "Maintaining a healthy lifestyle is essential for overall well-being. Here are 10 tips to help you stay healthy.", "/images/healthy-lifestyle.jpg", new DateTime(2026, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "10 Tips for a Healthy Lifestyle" },
                    { 3, "Noraly Knight", 3, "Discover the top travel destinations for 2026 and plan your next adventure.", "/images/travel-destinations.jpg", new DateTime(2026, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Top Travel Destinations for 2026" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentDate", "Content", "PostId", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Great introduction to ASP.NET Core! Thanks for sharing.", 1, "John Doe" },
                    { 2, new DateTime(2026, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "These tips are really helpful! I'm going to try implementing them.", 2, "Jane Smith" },
                    { 3, new DateTime(2026, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "I can't wait to visit these travel destinations! Thanks for the recommendations.", 3, "Alice Johnson" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts");

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
