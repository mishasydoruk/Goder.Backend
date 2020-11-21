using System.ComponentModel.DataAnnotations;

namespace Goder.BL.DTO
{
    public class FirebaseUserDTO
    {
        [Required]
        public string DisplayName { get; set; }
        public string PhotoURL { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
