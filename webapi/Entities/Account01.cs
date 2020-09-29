// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using Newtonsoft.Json;

// namespace webapi.Entities
// {
//     public class Account
//     {
//         public int Id { get; set; }
//         public string FirstName { get; set; }
//         public string LastName { get; set; }
//         public string Email { get; set; }

//         [JsonProperty("role")]
//         [Required(ErrorMessage = "role is required.")]
//         public Role Role { get; set; }
//         public string PartyName { get; set; }
//         public string Category { get; set; }
//         public string Description { get; set; }
//         public string IdentityId { get; set; }

//          public DateTime Created { get; set; }
//         public DateTime? Updated { get; set; }

//     }
// }