using System.Linq;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Infrastracture.Db;
using AppCore.Models;
using Infrastracture.Repositoreis;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Testy
{
    public class BookTests : IClassFixture<CustomWebApplicationFactory<BookstoreApi.Program>>
    {
        private readonly CustomWebApplicationFactory<BookstoreApi.Program> _factory;

        public BookTests(CustomWebApplicationFactory<BookstoreApi.Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<gravity_booksContext>();

            // Act
            var bookCount = context.book.Count();

            // Assert
            Assert.True(bookCount >= 2);
        }

        [Fact]
        public async Task CreateBook_Should_Create_Book_In_Memory_Database()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<gravity_booksContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Create a new instance of the in-memory database context
            using (var context = new gravity_booksContext(options))
            {
                // Create a mock ILogger
                var loggerMock = new Mock<ILogger<BookRepositories>>();

                // Create an instance of the BookRepositories class with the in-memory context and mock logger
                var repository = new BookRepositories(context, loggerMock.Object);

                // Create a new book object
                var newBook = new book
                {
                    title = "New Book Title",
                    isbn13 = "sfaf",
                    language_id = 2,
                    num_pages = 20,
                    publisher_id = 4
                };

                // Act: Attempt to create the book
                var result = await repository.CreateBook(newBook);

                // Assert: Check if the book creation was successful
                Assert.True(result > 0, "The book creation operation should return a positive identifier for the newly created book.");

                // Additional assertions to verify the state of the database
                Assert.Equal(1, context.book.Count());
                var createdBook = await context.book.FindAsync(result);
                Assert.NotNull(createdBook);
                Assert.Equal(newBook.title, createdBook.title);
            }
        }
    }
}
