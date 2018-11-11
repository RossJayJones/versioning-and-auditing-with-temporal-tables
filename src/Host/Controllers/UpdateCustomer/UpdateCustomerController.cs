using System.Threading.Tasks;
using Domain;
using Host.Controllers.CreateAddress.Results;
using Host.Controllers.UpdateCustomer.Models;
using Host.Infrastructure.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Host.Controllers.UpdateCustomer
{
    [ApiController]
    [Route("api")]
    public class UpdateCustomerController : ControllerBase
    {
        private readonly DomainDbContext _db;

        public UpdateCustomerController(DomainDbContext db)
        {
            _db = db;
        }

        [ProducesResponseType(typeof(CreateAddressResult), 204)]
        [Route("customers/{customerId}")]
        [HttpPut]
        public async Task<IActionResult> Execute(int customerId, UpdateCustomerModel data)
        {
            var customer = await _db.Set<Customer>().SingleAsync(p => p.Id == customerId);
            customer.Update(name: data.Name);
            return NoContent();
        }
    }
}
