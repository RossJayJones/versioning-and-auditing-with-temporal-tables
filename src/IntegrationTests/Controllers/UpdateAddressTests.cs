using System.Net;
using System.Threading.Tasks;
using Domain;
using Host.Controllers.CreateAddress.Models;
using IntegrationTests.Fixtures;
using Microsoft.EntityFrameworkCore;
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
        public async Task ItShouldUpdateTheAddress()
        {
            var customerId = await CreateCustomer();
            var addressId = await CreateAddress(customerId);
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

        private async Task<int> CreateCustomer()
        {
            using (var db = _fixture.CreateDbContext())
            {
                var customer = new Customer("Sample customer");
                await db.Set<Customer>().AddAsync(customer);
                await db.SaveChangesAsync();
                return customer.Id;
            }
        }

        private async Task<int> CreateAddress(int customerId)
        {
            using (var db = _fixture.CreateDbContext())
            {
                var customer = await db.Set<Customer>().SingleAsync(c => c.Id == customerId);
                var address = customer.AddAddress(
                    line: "line",
                    suburb: "suburb",
                    city: "city",
                    province: "province",
                    code: "1234");
                await db.SaveChangesAsync();
                return address.Id;
            }
        }
    }
}
