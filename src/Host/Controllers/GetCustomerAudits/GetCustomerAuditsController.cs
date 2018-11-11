using System.Linq;
using System.Threading.Tasks;
using Host.Controllers.GetCustomerAudits.Results;
using Host.Infrastructure.Query;
using Host.Infrastructure.Query.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers.GetCustomerAudits
{
    [ApiController]
    [Route("api")]
    public class GetCustomerAuditsController : ControllerBase
    {
        private readonly GetCustomerAuditsQuery _query;

        public GetCustomerAuditsController(GetCustomerAuditsQuery query)
        {
            _query = query;
        }

        [Route("customers/{customerId}/audits")]
        [HttpGet]
        public async Task<IActionResult> Execute(int customerId)
        {
            var dtos = await _query.Execute(customerId);
            var results = dtos.Select(CreateGetCustomerAuditsResult);
            return Ok(results);
        }

        private GetCustomerAuditsResult CreateGetCustomerAuditsResult(CustomerAuditDto dto)
        {
            var result = new GetCustomerAuditsResult
            {
                Id = dto.Id,
                Timestamp = dto.Timestamp,
                Messages = dto.Messages
            };
            return result;
        }
    }
}
