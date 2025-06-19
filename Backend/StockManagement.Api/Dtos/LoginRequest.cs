using System.ComponentModel.DataAnnotations;

namespace StockManagement.Api.Dtos
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
