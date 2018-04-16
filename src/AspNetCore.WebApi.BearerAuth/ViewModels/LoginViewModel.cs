using System.ComponentModel.DataAnnotations;

namespace AspNetCore.WebApi.BearerAuth.ViewModels
{

    /// <summary>
    /// View Model para realização de Login
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Email usado para o login
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }

    }
}
