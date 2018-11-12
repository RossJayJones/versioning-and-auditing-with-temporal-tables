using System.Linq;
using System.Threading.Tasks;
using Host.Controllers.GetCustomer.Results;
using Host.Infrastructure.Query;
using Host.Infrastructure.Query.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers.GetCustomer
{
    [ApiController]
    [Route("api")]
    public class GetCustomerController : ControllerBase
    {
        private readonly GetCustomerByIdQuery _query;

        public GetCustomerController(GetCustomerByIdQuery query)
        {
            _query = query;
        }

        [Route("customers/{customerId}")]
        [HttpGet]
        public async Task<IActionResult> ByAuditId(int customerId, int? auditId)
        {
            var dto = await _query.Execute(customerId, auditId);
            var results = CreateCustomerResult(dto);
            return Ok(results);
        }

        private static GetCustomerResult CreateCustomerResult(CustomerDto dto)
        {
            var result = new GetCustomerResult
            {
                Name = dto.Name,
                Addresses = dto.Addresses.Select(CreateAddressResult).ToList(),
                Audits = dto.Audits.Select(CreateAuditResult).ToList()
            };
            return result;
        }

        private static GetCustomerAddressResult CreateAddressResult(AddressDto dto)
        {
            var result = new GetCustomerAddressResult
            {
                Id = dto.Id,
                City = dto.City,
                Code = dto.Code,
                Line = dto.Line,
                Province = dto.Province,
                Suburb = dto.Suburb
            };
            return result;
        }

        private static GetCustomerAuditResult CreateAuditResult(CustomerAuditDto dto)
        {
            var result = new GetCustomerAuditResult
            {
                Id = dto.Id,
                Messages = dto.Messages.ToList(),
                Timestamp = dto.Timestamp
            };
            return result;
        }
    }
}