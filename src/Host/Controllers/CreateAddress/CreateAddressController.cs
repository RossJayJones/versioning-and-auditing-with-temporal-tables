using System.Threading.Tasks;
using Domain;
using Host.Controllers.CreateAddress.Models;
using Host.Controllers.CreateAddress.Results;
using Host.Infrastructure.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Host.Controllers.CreateAddress
{
    [ApiController]
    [Route("api")]
    public class CreateAddressController : ControllerBase
    {
        private readonly DomainDbContext _db;

        public CreateAddressController(DomainDbContext db)
        {
            _db = db;
        }

        [ProducesResponseType(typeof(CreateAddressResult), 200)]
        [Route("customers/{customerId}/addresses")]
        [HttpPost]
        public async Task<IActionResult> Execute(int customerId, CreateAddressModel data)
        {
            var customer = await _db.Set<Customer>().Include(p => p.Addresses).SingleAsync(p => p.Id == customerId);
            var address = customer.AddAddress(
                line: data.Line,
                suburb: data.Suburb,
                city: data.City,
                province: data.Province,
                code: data.Code);
            await _db.SaveChangesAsync();
            var result = new CreateAddressResult { Id = address.Id };
            return Ok(result);
        }
    }
}
