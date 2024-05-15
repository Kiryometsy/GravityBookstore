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

            using (var context = new gravity_booksContext(options))
            {
                // Create a mock ILogger
                var loggerMock = new Mock<ILogger<BookRepositories>>();

                var repository = new BookRepositories(context, loggerMock.Object);

                var newBook = new book
                {
                    title = "New Book Title",
                };

                // Act
                var result = await repository.CreateBook(newBook);

                // Assert
                Assert.NotEqual(0, result); 
            }
        }
    }
}
