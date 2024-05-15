using System.Net;
using AppCore.Dto;
using AppCore.Filters;
using BookstoreApi;
using BookstoreApi.Controllers;
using Infrastracture.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

namespace Testy
{
    public class Test1
    {
        [Fact]
        public async Task Get_ReturnsOkResult_WithListOfBooks()
        {
            // Arrange
            var mockBookService = new Mock<IBookService>();
            var controller = new booksController(mockBookService.Object);
            var expectedBooks = new List<BookDto>
            {
                new BookDto { BookId = 1, Title = "Book 1" },
                new BookDto { BookId = 2, Title = "Book 2" }
            };
            mockBookService.Setup(service => service.Get(It.IsAny<BookFilter>())).ReturnsAsync(expectedBooks);

            // Act
            var actionResult = await controller.Get(new BookFilter());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var resultBooks = Assert.IsAssignableFrom<List<BookDto>>(okResult.Value);
            Assert.Equal(expectedBooks.Count, resultBooks.Count);
        }

        [Fact]
        public async void GetShouldReturnOkStatus()
        {
            //Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            //Act
            var result = await client.GetAsync("/api/books");

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Contains("application/json", result.Content.Headers.GetValues("Content-Type").First());
        }

        [Fact]
        public async Task Get_Returns10()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var result = await client.GetFromJsonAsync<List<BookDto>>("/api/books");

            Assert.Equal(10, result.Count);
        }
    }
}