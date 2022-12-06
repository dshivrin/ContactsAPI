using ContactsAPI.Attributes;
using ContactsAPI.Data.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [SocialNumberValidator]
        public int SocialNumber { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2)]
        public string Name { get; set; } = string.Empty;
        [EmailValidator]
        public string Email { get; set; } = string.Empty;
        [DateValidator]
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        [PhoneValidator]
        public string Phone { get; set; } = string.Empty;
    }
}
