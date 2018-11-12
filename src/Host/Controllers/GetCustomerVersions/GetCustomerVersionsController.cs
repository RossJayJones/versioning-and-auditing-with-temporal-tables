using System.Linq;
using System.Threading.Tasks;
using Host.Controllers.GetCustomerVersions.Results;
using Host.Infrastructure.Query;
using Host.Infrastructure.Query.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers.GetCustomerVersions
{
    [ApiController]
    [Route("api")]
    public class GetCustomerVersionsController : ControllerBase
    {
        private readonly GetVersionsQuery _query;

        public GetCustomerVersionsController(GetVersionsQuery query)
        {
            _query = query;
        }

        [Route("customers/{customerId}/versions")]
        [HttpGet]
        public async Task<IActionResult> Execute(int customerId)
        {
            var dtos = await _query.Execute(customerId);
            var results = dtos.Select(CreateGetCustomerVersionsResult);
            return Ok(results);
        }

        private GetCustomerVersionsResult CreateGetCustomerVersionsResult(CustomerVersionDto dto)
        {
            var result = new GetCustomerVersionsResult
            {
                Id = dto.Id,
                Timestamp = dto.Timestamp,
                Message = dto.Message
            };
            return result;
        }
    }
}
