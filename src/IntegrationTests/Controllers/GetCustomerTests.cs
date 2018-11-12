using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Host.Controllers.GetCustomer.Results;
using IntegrationTests.Fixtures;
using Xunit;

namespace IntegrationTests.Controllers
{
    public class GetCustomerTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;

        public GetCustomerTests(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ItShouldReturnOk()
        {
            var customerId = await _fixture.CreateCustomer();
            await _fixture.CreateAddress(customerId);

            var response = await _fixture.Get($"api/customers/{customerId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public class WhenAuditIdProvided : IClassFixture<TestServerFixture>
        {
            private readonly TestServerFixture _fixture;

            public WhenAuditIdProvided(TestServerFixture fixture)
            {
                _fixture = fixture;
            }

            [Fact]
            public async Task ItShouldReturnOk()
            {
                var customerId = await _fixture.CreateCustomer();

                var response = await _fixture.Get($"api/customers/{customerId}");
                var result = await _fixture.Deserialize<GetCustomerResult>(response);
                response = await _fixture.Get($"api/customers/{customerId}?auditId={result.Audits.First().Id}");

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}