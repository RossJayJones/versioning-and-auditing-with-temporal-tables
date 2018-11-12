using System.Threading.Tasks;
using Domain;
using Host.Controllers.CreateAddress.Results;
using Host.Controllers.CreateVersion.Models;
using Host.Infrastructure.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Host.Controllers.CreateVersion
{
    [ApiController]
    [Route("api")]
    public class CreateVersionController : ControllerBase
    {
        private readonly DomainDbContext _db;

        public CreateVersionController(DomainDbContext db)
        {
            _db = db;
        }

        [ProducesResponseType(typeof(CreateAddressResult), 200)]
        [Route("customers/{customerId}/versions")]
        [HttpPost]
        public async Task<IActionResult> Execute(int customerId, CreateVersionModel data)
        {
            var customer = await _db.Set<Customer>().SingleAsync(p => p.Id == customerId);
            customer.IncrementVersion(data.Message);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
