using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class Party
    {
        public long PartyId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}