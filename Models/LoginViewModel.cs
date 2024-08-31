using System.ComponentModel.DataAnnotations;

namespace ITService.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string LoginType { get; set; }  // Bu alanı ekleyin
    }

}
