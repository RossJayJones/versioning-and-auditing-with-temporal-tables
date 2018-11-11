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
        private readonly GetCustomerByAuditIdQuery _getCustomerByAuditId;
        private readonly GetCustomerByVersionIdQuery _getCustomerByVersionId;

        public GetCustomerController(GetCustomerByAuditIdQuery getCustomerByAuditId,
            GetCustomerByVersionIdQuery getCustomerByVersionId)
        {
            _getCustomerByAuditId = getCustomerByAuditId;
            _getCustomerByVersionId = getCustomerByVersionId;
        }

        [Route("customers/{customerId}/audits/{auditId}")]
        [HttpGet]
        public async Task<IActionResult> ByAuditId(int customerId, int auditId)
        {
            var dto = await _getCustomerByAuditId.Execute(customerId, auditId);
            var results = CreateGetCustomerResult(dto);
            return Ok(results);
        }

        [Route("customers/{customerId}/versions/{versionId}")]
        [HttpGet]
        public async Task<IActionResult> ByVersionId(int customerId, int versionId)
        {
            var dto = await _getCustomerByVersionId.Execute(customerId, versionId);
            var results = CreateGetCustomerResult(dto);
            return Ok(results);
        }

        private GetCustomerResult CreateGetCustomerResult(CustomerDto dto)
        {
            var result = new GetCustomerResult
            {
                Id = dto.Id,
                Name = dto.Name,
                Addresses = dto.Addresses.Select(CreateGetCustomerAddressResult).ToList(),
            };
            return result;
        }

        private GetCustomerAddressResult CreateGetCustomerAddressResult(AddressDto dto)
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
    }
}