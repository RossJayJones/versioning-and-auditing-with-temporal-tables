using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain;
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
        public async Task ItShouldCreateAddress()
        {
            (int customerId, int addressId) = await GetCustomerAndAddressId();
            var data = new CreateAddressModel
            {
                Line = "line",
                City = "city",
                Code = "1234",
                Province = "province",
                Suburb = "suburb"
            };

            var response = await _fixture.Put($"api/customers/{customerId}/addresses/{addressId}", data);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        private async Task<(int CustomerId, int AddressId)> GetCustomerAndAddressId()
        {
            using (var db = _fixture.CreateDbContext())
            {
                var customer = new Customer("Sample customer");
                await db.Set<Customer>().AddAsync(customer);
                var address = customer.AddAddress(
                    line: "line",
                    suburb: "suburb",
                    city: "city",
                    province: "province",
                    code: "1234");
                await db.SaveChangesAsync();
                return (customer.Id, address.Id);
            }
        }
    }
}
