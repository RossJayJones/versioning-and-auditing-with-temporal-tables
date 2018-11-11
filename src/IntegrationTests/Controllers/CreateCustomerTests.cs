using System.Net;
using System.Threading.Tasks;
using Host.Controllers.CreateCustomer.Models;
using IntegrationTests.Fixtures;
using Xunit;

namespace IntegrationTests.Controllers
{
    public class CreateCustomerTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;

        public CreateCustomerTests(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ItShouldCreateCustomer()
        {
            var data = new CreateCustomerModel { Name = "Sample Customer" };

            var response = await _fixture.Post("api/customers", data);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
