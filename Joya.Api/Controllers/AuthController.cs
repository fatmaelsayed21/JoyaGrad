using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Domain.Models;
using Joya.Api.Dtos;
using Joya.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Joya.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;

        public AuthController(UserManager<User> userManager ,SignInManager<User> signInManager, IConfiguration configuration, EmailService emailService)
        {
           _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        #region GenerateJwtTokenMethod
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                 new Claim(JwtRegisteredClaimNames.Email, user.Email),
                 new Claim(ClaimTypes.Name, user.UserName),
                 new Claim(ClaimTypes.Email, user.Email),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("s7b@1X!z4eW#9rLpQzVt3$YgMnKx2#Hv"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Joya.com",
                audience: "Joya.com",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        } 
        #endregion



        #region SignUp
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if username or email already exists
            var existingUserByUsername = await _userManager.FindByNameAsync(model.UserName);
            if (existingUserByUsername != null)
                return BadRequest("Username already exists.");

            var existingUserByEmail = await _userManager.FindByEmailAsync(model.Email);
            if (existingUserByEmail != null)
                return BadRequest("Email already exists.");

           
            // Create user
            var user = new User
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return BadRequest(ModelState);
            }
            var validRoles = new[] { "Buyer", "Seller" };
            if (!validRoles.Contains(model.Role, StringComparer.OrdinalIgnoreCase))
                return BadRequest("Invalid role.");

            
            await _userManager.AddToRoleAsync(user, model.Role);

            return Ok(new { message = "User registered successfully." });
        }
        #endregion

        #region SignIn

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized("Invalid email or password.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Invalid email or password.");


            var token = GenerateJwtToken(user);

            return Ok(new
            {
                Message = "Login successful",
                Token = token
            });
        }
        #endregion

        #region SignOut

        [HttpPost("SignOut")]
        [Authorize] // to make sure you are signed in 
        public IActionResult SignOut()
        {
            return Ok(new { message = "Signed out successfully. Please remove the token on the client side." });
        }

        #endregion

        #region ForgetPassword
        [HttpPost("SendResetPasswordUrl")]
        public async Task<IActionResult> SendResetPasswordUrl([FromBody] ForgetPasswordDto model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("User not found.");


            var token = await _userManager.GeneratePasswordResetTokenAsync(user);


            var encodedToken = WebUtility.UrlEncode(token);


            var email = new Email
            {
                To = model.Email,
                Subject = "Reset Your Password",
                Body = $"Here is your password reset token:\n\nToken: {encodedToken}\n\nUse this token in the reset password form on the website when it becomes available."
            };


            var result = _emailService.SendEmail(email);


            return result ? Ok("Reset email sent") : StatusCode(500, "Failed to send email.");
        }

        #endregion


        #region ResetPassword
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("User not found.");

            var decodedToken = WebUtility.UrlDecode(model.Token);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return BadRequest(ModelState);
            }

            return Ok("Password has been reset successfully.");
        } 
        #endregion


    }
}




    

