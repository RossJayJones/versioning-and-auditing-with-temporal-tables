
using System.ComponentModel.DataAnnotations;

namespace Host.Controllers.CreateCustomer.Models
{
    public class CreateCustomerModel
    {
        [Required]
        public string Name { get; set; }
    }
}
