using System.Net;
using System.Threading.Tasks;
using Host.Controllers.CreateVersion.Models;
using Host.Controllers.GetCustomer.Results;
using IntegrationTests.Fixtures;
using Xunit;

namespace IntegrationTests.Controllers
{
    public class CreateVersionTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;

        public CreateVersionTests(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ItShouldReturnNoContent()
        {
            var customerId = await _fixture.CreateCustomer();
            var data = new CreateVersionModel {Message = "A new version"};

            var response = await _fixture.Post($"api/customers/{customerId}/versions", data);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
