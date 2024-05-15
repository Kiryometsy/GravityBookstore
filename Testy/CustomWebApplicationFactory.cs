using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infrastracture.Db;
using System;
using System.Linq;
using AppCore.Models;

namespace Testy
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private bool _isSeeded = false;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<gravity_booksContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                if (environment == "Testing")
                {
                    services.AddDbContext<gravity_booksContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });
                }
                else
                {
                    var connectionString = "Host=localhost;Database=gravity_books;Username=postgres;Password=123";
                    services.AddDbContext<gravity_booksContext>(options =>
                    {
                        options.UseNpgsql(connectionString);
                    });
                }

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<gravity_booksContext>();

                    if (!_isSeeded)
                    {
                        try
                        {
                            context.Database.EnsureCreated();
                            SeedTestData(context);
                            _isSeeded = true;
                        }
                        catch (Exception ex)
                        {
                            // Log errors or do something else
                            throw;
                        }
                    }
                }
            });
        }

        private static void SeedTestData(gravity_booksContext context)
        {
            // Check if the database is already seeded
            if (context.book_author.Any())
            {
                return; // Data already seeded, no need to proceed
            }

            // Add test data for authors
            var author1 = new author { author_id = 1, author_name = "Author 1" };
            var author2 = new author { author_id = 2, author_name = "Author 2" };

            context.author.AddRange(author1, author2);

            // Add test data for books
            var book1 = new book { book_id = 1, title = "Book 1" };
            var book2 = new book { book_id = 2, title = "Book 2" };

            context.book.AddRange(book1, book2);

            // Seed data for book_author
            var bookAuthor1 = new book_author { book_id = 1, author_id = author1.author_id };
            var bookAuthor2 = new book_author { book_id = 2, author_id = author2.author_id };

            context.book_author.AddRange(bookAuthor1, bookAuthor2);

            // Save changes
            context.SaveChanges();
        }


    }
}
