using System.ComponentModel.DataAnnotations;

namespace Host.Controllers.CreateVersion.Models
{
    public class CreateVersionModel
    {
        [Required]
        public string Message { get; set; }
    }
}
