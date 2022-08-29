using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Mvc.Dtos.AccountDtos
{
    public class UserLoginDto
    {
        [Required]
        [Display]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Display]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
