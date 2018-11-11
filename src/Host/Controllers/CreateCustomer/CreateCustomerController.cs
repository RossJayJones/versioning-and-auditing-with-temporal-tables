using System.Threading.Tasks;
using Domain;
using Host.Controllers.CreateCustomer.Models;
using Host.Controllers.CreateCustomer.Results;
using Host.Infrastructure.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers.CreateCustomer
{
    [ApiController]
    [Route("api")]
    public class CreateCustomerController : ControllerBase
    {
        private readonly DomainDbContext _db;

        public CreateCustomerController(DomainDbContext db)
        {
            _db = db;
        }

        [ProducesResponseType(typeof(CreateCustomerResult), 200)]
        [Route("customers")]
        [HttpPost]
        public async Task<IActionResult> Execute(CreateCustomerModel data)
        {
            var customer = new Customer(data.Name);
            await _db.AddAsync(customer);
            await _db.SaveChangesAsync();
            var result = new CreateCustomerResult {Id = customer.Id, Name = customer.Name};
            return Ok(result);
        }
    }
}
