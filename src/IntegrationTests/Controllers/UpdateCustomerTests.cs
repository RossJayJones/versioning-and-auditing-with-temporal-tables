using System.Net;
using System.Threading.Tasks;
using Domain;
using Host.Controllers.UpdateCustomer.Models;
using IntegrationTests.Fixtures;
using Xunit;

namespace IntegrationTests.Controllers
{
    public class UpdateCustomerTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;

        public UpdateCustomerTests(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ItShouldUpdateTheCustomer()
        {
            var customerId = await GetCustomerId();
            var data = new UpdateCustomerModel { Name = "New name" };

            var response = await _fixture.Put($"api/customers/{customerId}", data);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        private async Task<int> GetCustomerId()
        {
            using (var db = _fixture.CreateDbContext())
            {
                var customer = new Customer("Sample customer");
                await db.Set<Customer>().AddAsync(customer);
                await db.SaveChangesAsync();
                return customer.Id;
            }
        }
    }
}
