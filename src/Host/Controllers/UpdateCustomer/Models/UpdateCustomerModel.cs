
using System.ComponentModel.DataAnnotations;

namespace Host.Controllers.UpdateCustomer.Models
{
    public class UpdateCustomerModel
    {
        [Required]
        public string Name { get; set; }
    }
}
