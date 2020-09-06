// using System.ComponentModel.DataAnnotations;
// using Newtonsoft.Json;

// namespace webapi.Models
// {
//     public class User
//     {
//         public int Id { get; set; }
//         [Required]
//         public string UserName { get; set; }
//         [Required]
//         public string Email { get; set; }
//         public string Phone { get; set; }
//         public string FirstName { get; set; }
//         public string LastName { get; set; }

//         [JsonProperty("role")]
//         [Required(ErrorMessage = "role is required.")]
//         public Role Role { get; set; }
//         public string PartyName { get; set; }
//         public string Category { get; set; }
//         public string Description { get; set; }
//         public string IdentityId { get; set; }
//     }
// }