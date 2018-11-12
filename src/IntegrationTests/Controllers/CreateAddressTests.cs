using System.Net;
using System.Threading.Tasks;
using Host.Controllers.CreateAddress.Models;
using IntegrationTests.Fixtures;
using Xunit;

namespace IntegrationTests.Controllers
{
    public class CreateAddressTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;

        public CreateAddressTests(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ItShouldReturnOk()
        {
            var customerId = await _fixture.CreateCustomer();
            var data = new CreateAddressModel
            {
                Line = "line",
                City = "city",
                Code = "1234",
                Province = "province",
                Suburb = "suburb"
            };

            var response = await _fixture.Post($"api/customers/{customerId}/addresses", data);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}