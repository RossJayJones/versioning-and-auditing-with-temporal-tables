using System.Net;
using System.Threading.Tasks;
using Host.Controllers.CreateAddress.Models;
using IntegrationTests.Fixtures;
using Xunit;

namespace IntegrationTests.Controllers
{
    public class UpdateAddressTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;

        public UpdateAddressTests(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ItShouldReturnNoContent()
        {
            var customerId = await _fixture.CreateCustomer();
            var addressId = await _fixture.CreateAddress(customerId);
            var data = new CreateAddressModel
            {
                Line = "1st Rd",
                City = "Johannesburg",
                Code = "000",
                Province = "Gauteng",
                Suburb = "Centurion"
            };

            var response = await _fixture.Put($"api/customers/{customerId}/addresses/{addressId}", data);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
