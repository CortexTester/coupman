using System.ComponentModel.DataAnnotations;

namespace webapi.Entities
{
    public class Category : Entity
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public Attachment Image { get; set; }
    }
}