using System.Threading.Tasks;
using Domain;
using Host.Controllers.CreateAddress.Models;
using Host.Infrastructure.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Host.Controllers.UpdateAddress
{
    [ApiController]
    [Route("api")]
    public class UpdateAddressController : ControllerBase
    {
        private readonly DomainDbContext _db;

        public UpdateAddressController(DomainDbContext db)
        {
            _db = db;
        }

        [ProducesResponseType(204)]
        [Route("customers/{customerId}/addresses/{addressId}")]
        [HttpPut]
        public async Task<IActionResult> Execute(int customerId, int addressId, CreateAddressModel data)
        {
            var customer = await _db.Set<Customer>().Include(p => p.Addresses).SingleAsync(p => p.Id == customerId);
            customer.UpdateAddress(addressId: addressId,
                line: data.Line,
                suburb: data.Suburb,
                city: data.City,
                province: data.Province,
                code: data.Code);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}