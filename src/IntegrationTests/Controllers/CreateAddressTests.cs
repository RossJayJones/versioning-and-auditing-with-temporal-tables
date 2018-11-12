using System.Net;
using System.Threading.Tasks;
using Domain;
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
        public async Task ItShouldCreateTheAddress()
        {
            var customerId = await GetCustomerId();
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
