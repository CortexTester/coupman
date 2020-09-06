using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}