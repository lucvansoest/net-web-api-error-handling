using System.ComponentModel.DataAnnotations;

namespace WebApiErrorHandling.Models
{
    public class UserCreateDto
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
