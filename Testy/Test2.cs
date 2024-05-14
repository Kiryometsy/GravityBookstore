using BookstoreApi;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Testy
{
    public class Test2
    {
        [Fact]
        public async void GetShouldReturnOkStatus()
        {
            //Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            //Act
            var result = await client.GetAsync("/api/customer");

            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Contains("application/json", result.Content.Headers.GetValues("Content-Type").First());
        }
    }
}
