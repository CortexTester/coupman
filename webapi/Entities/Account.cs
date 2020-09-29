using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webapi.Entities
{
    public enum AccountStatus
    {
        Pending,
        Acvtive,
        Rejected


    }
    public class Account : Entity
    {
        public int AccountId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public string IdentityId { get; set; }

        public string Description { get; set; }

        public ICollection<Attachment> Attachments { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }

        public string ContactPerson { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }

        public ICollection<AccountCity> AccountCities { get; set; } = new List<AccountCity>();
        public ICollection<SocialLink> SocialLinks { get; set; } = new List<SocialLink>();

        public Attachment Logo { get; set; }
        public ICollection<AccountCategory> AccountCategories { get; set; } = new List<AccountCategory>();

        public AccountStatus Status { get; set; }

        public Role Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}