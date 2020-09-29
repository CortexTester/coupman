using System.ComponentModel.DataAnnotations;

namespace webapi.Entities
{
    public enum SocialName
    {
        FaceBook,
        Twitter,
        GooglePlus ,
        Instagram,
        Vimeo,
        YouTube,
        LinkedIn,
        Dribbble,
        Skype,
        Foursquare,
        Behance 

    }
    public class SocialLink : Entity
    {
        public int SocialLinkId { get; set; }
        [Required]
        public SocialName SocialName { get; set; }
        [Required]
        public string Link { get; set; }

    }
}