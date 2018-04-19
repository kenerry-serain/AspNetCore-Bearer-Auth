using AspNetCore.WebApi.BearerAuth.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AspNetCore.WebApi.BearerAuth.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        public readonly IConfiguration Configuration;

        public AuthController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        /// <summary>
        /// Method to authenticate user
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginViewModel login)
        {
            /* Login validating */
            if (IsLoginValid(login))
            {
                /* Adding user claims */
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, "71"), /* Insert user id here */
                    new Claim(JwtRegisteredClaimNames.Email, login.Email)
                };

                /* Creating signing credentials */
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                /* Creating JWT Token */
                var token = new JwtSecurityToken
                (
                    claims: claims,
                    signingCredentials: creds,
                    expires: DateTime.Now.AddMinutes(5), /* Token expire time */
                    issuer: Configuration["AuthSettings:Issuer"],
                    audience: Configuration["AuthSettings:Audience"]
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return BadRequest(new
            {
                error = "Not authorized user. Please verify email and password."
            });
        }


        /// <summary>
        /// Endpoint used for verify token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(new
            {
                success = "The token is working!"
            });
        }

        /// <summary>
        /// Method used for validate user login 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        private bool IsLoginValid(LoginViewModel login)
        {
            /* Plugin your user service validation here */
            if (ModelState.IsValid && login.Email == "authenticateduser@aspnetcore.com.br" && login.Password == "123quatro")
                return true;

            return false;
        }
    }
}
