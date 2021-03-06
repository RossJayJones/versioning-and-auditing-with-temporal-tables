﻿using System.Net;
using System.Threading.Tasks;
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
        public async Task ItShouldReturnNoContent()
        {
            var customerId = await _fixture.CreateCustomer();
            var data = new UpdateCustomerModel { Name = "New name" };

            var response = await _fixture.Put($"api/customers/{customerId}", data);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
