using System.Linq;
using System.Threading.Tasks;
using Host.Controllers.GetCustomerVersion.Results;
using Host.Infrastructure.Query;
using Host.Infrastructure.Query.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers.GetCustomerVersion
{
    [ApiController]
    [Route("api")]
    public class GetCustomerVersionController : ControllerBase
    {
        private readonly GetCustomerByVersionIdQuery _query;

        public GetCustomerVersionController(GetCustomerByVersionIdQuery query)
        {
            _query = query;
        }

        [Route("customers/{customerId}/versions/{versionId}")]
        [HttpGet]
        public async Task<IActionResult> Execute(int customerId, int versionId)
        {
            var dto = await _query.Execute(customerId, versionId);
            var results = CreateCustomerResult(dto);
            return Ok(results);
        }

        public static GetCustomerVersionResult CreateCustomerResult(CustomerDto dto)
        {
            var result = new GetCustomerVersionResult
            {
                Name = dto.Name,
                Addresses = dto.Addresses.Select(CreateAddressResult).ToList(),
            };
            return result;
        }

        public static GetCustomerVersionAddressResult CreateAddressResult(AddressDto dto)
        {
            var result = new GetCustomerVersionAddressResult
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
    }
}