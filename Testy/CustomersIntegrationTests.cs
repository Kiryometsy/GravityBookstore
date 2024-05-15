using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using AppCore.Dto;
using Microsoft.AspNetCore.Hosting;
using Testy;
using Infrastracture.Db;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace IntegrationTests
{
    public class CustomersIntegrationTests : IClassFixture<CustomWebApplicationFactory<BookstoreApi.Program>>
    {
        private readonly HttpClient _client;
        private readonly gravity_booksContext _context;

        public CustomersIntegrationTests(CustomWebApplicationFactory<BookstoreApi.Program> factory)
        {
            _client = factory.CreateClient();
            _context = factory.Services.GetRequiredService<gravity_booksContext>();
        }

        [Fact]
        public async Task Post_ValidCustomer_ShouldCreateNewRecords()
        {
            // Arrange
            var customerDto = new
            {
                firstName = "John",
                lastName = "Doe",
                email = "john.doe@example.com",
                address = new
                {
                    streetNumber = "123",
                    streetName = "Main St",
                    city = "City",
                    countryId = 1
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(customerDto), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/customers", content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic responseData = JsonConvert.DeserializeObject(responseContent);

            // Extract customer ID from response content
            int customerId = responseData.customerId;

            // Assert that customer ID is greater than 0
            Assert.True(customerId > 0, "Customer ID should be greater than 0.");
        }

        [Fact]
        public async Task Post_InvalidCountryId_ShouldReturnBadRequest()
        {
            // Arrange
            var invalidCustomer = new CustomerDto
            {
                firstName = "Jane",
                lastName = "Doe",
                email = "jane.doe@example.com",
                address = new AddressDto
                {
                    streetNumber = "456",
                    streetName = "Oak Street",
                    city = "Town",
                    countryId = 999
                }
            };

            // Act
            var response = await _client.PostAsync("/api/customers", new StringContent(JsonConvert.SerializeObject(invalidCustomer), Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
