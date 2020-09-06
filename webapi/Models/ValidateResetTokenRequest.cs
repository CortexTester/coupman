using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Token { get; set; }
    }
}