using System.ComponentModel.DataAnnotations;

namespace AspNetCore.WebApi.BearerAuth.ViewModels
{

    /// <summary>
    /// View Model to Login
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Email to login
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Password to login
        /// </summary>
        [Required]
        public string Password { get; set; }

    }
}
