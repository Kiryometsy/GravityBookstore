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
            var validCustomer = new CustomerDto
            {
                firstName = "John",
                lastName = "Doe",
                email = "john.doe@example.com",
                address = new AddressDto
                {
                    streetNumber = "123",
                    streetName = "Main Street",
                    city = "City",
                    countryId = 1
                }
            };

            // Act
            var response = await _client.PostAsync("/api/customers", new StringContent(JsonConvert.SerializeObject(validCustomer), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            // Deserialize response to get the newly created customer ID
            var customerId = int.Parse(await response.Content.ReadAsStringAsync());

            // Query the database to check if the customer and address exist
            var customerExists = await _context.customer.AnyAsync(c => c.customer_id == customerId);
            var addressExists = await _context.address.AnyAsync(a => a.customer_address.Any(ca => ca.customer_id == customerId));

            // Assert that both customer and address exist
            Assert.True(customerExists);
            Assert.True(addressExists);
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
                    countryId = 999 // Assuming 999 is an invalid country ID
                }
            };

            // Act
            var response = await _client.PostAsync("/api/customer", new StringContent(JsonConvert.SerializeObject(invalidCustomer), Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
