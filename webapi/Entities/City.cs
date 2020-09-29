using System.ComponentModel.DataAnnotations;

namespace webapi.Entities
{
    public class City : Entity
    {
        public int CityId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
    }
}