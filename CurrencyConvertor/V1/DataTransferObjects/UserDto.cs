using System.ComponentModel.DataAnnotations;

namespace CurrencyConvertor.V1.DataTransferObjects
{
    public class UserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
