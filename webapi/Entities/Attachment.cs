using System.ComponentModel.DataAnnotations;

namespace webapi.Entities
{
    public class Attachment : Entity
    {
        public int AttachmentId { get; set; }
        [Required]
        public string Url { get; set; }
        [MaxLength(50)]
        public string AttachmentType { get; set; }

        public string Name { get; set; }


    }
}