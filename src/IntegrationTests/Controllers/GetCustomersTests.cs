using System.Net;
using System.Threading.Tasks;
using IntegrationTests.Fixtures;
using Xunit;

namespace IntegrationTests.Controllers
{
    public class GetCustomersTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;

        public GetCustomersTests(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ItShouldReturnOk()
        {
            var customerId = await _fixture.CreateCustomer();
            await _fixture.CreateAddress(customerId);

            var response = await _fixture.Get("api/customers");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}