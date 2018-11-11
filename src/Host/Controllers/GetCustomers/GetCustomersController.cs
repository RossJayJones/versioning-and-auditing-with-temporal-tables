using System.Linq;
using System.Threading.Tasks;
using Host.Controllers.GetCustomers.Results;
using Host.Infrastructure.Query;
using Host.Infrastructure.Query.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers.GetCustomers
{
    [ApiController]
    [Route("api")]
    public class GetCustomersController : ControllerBase
    {
        private readonly GetCustomersQuery _query;

        public GetCustomersController(GetCustomersQuery query)
        {
            _query = query;
        }

        [Route("customers")]
        [HttpGet]
        public async Task<IActionResult> Execute()
        {
            var dtos = await _query.Execute();
            var results = dtos.Select(CreateGetCustomersResult);
            return Ok(results);
        }

        private GetCustomersResult CreateGetCustomersResult(CustomerDto dto)
        {
            var result = new GetCustomersResult
            {
                Id = dto.Id,
                Name = dto.Name
            };
            return result;
        }
    }
}
