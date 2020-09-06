// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Cors;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using webapi.Models;

// namespace webapi.Controllers
// {
//     // [EnableCors("CorsApi")]
//     [ApiController]
//     [Route("[controller]")]
//     public class PartyController : ControllerBase
//     {
//         private static readonly string[] Summaries = new[]
//         {
//             "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//         };

//         private readonly ILogger<PartyController> _logger;
//         private readonly ICoupmanRepository coupmanRepository;

//         public PartyController(ILogger<PartyController> logger, ICoupmanRepository coupmanRepository)
//         {
//             _logger = logger;
//             this.coupmanRepository = coupmanRepository;
//         }

//         [HttpGet]
//         public IEnumerable<User> Get() => coupmanRepository.Users.ToArray();

//         [HttpGet("{id}")]
//         public User GetParty(long id) => coupmanRepository.Users.FirstOrDefault(p=>p.Id == id);
//     }
// }
