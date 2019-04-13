using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTOs
{
    public class UserForRegisterDto
    {
        [Required]
        public string userName { get; set; }
        [Required]
        [StringLength(6,MinimumLength=4,ErrorMessage="you must specify password between 6 and 4 char")]
        public string password { get; set; }
    }
}