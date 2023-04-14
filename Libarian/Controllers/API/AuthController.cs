using Azure;
using Librarian.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Librarian.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IConfiguration _configuration;

		public AuthController(
		   UserManager<ApplicationUser> userManager,
		   RoleManager<IdentityRole> roleManager,
		   IConfiguration configuration)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_configuration = configuration;
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] MLoginModel model)
		{

            var user = await _userManager.FindByNameAsync(model.Username);
			if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
			{
				var userRoles = await _userManager.GetRolesAsync(user);

				var authClaims = new List<Claim>
				{	
					new Claim(ClaimTypes.Name, user.UserName),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				};

				foreach (var userRole in userRoles)
				{
					authClaims.Add(new Claim(ClaimTypes.Role, userRole));
				}

				var token = GetToken(authClaims);
				var encryptedToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
				{
					token = encryptedToken,
					expiration = token.ValidTo,
					Role = userRoles.FirstOrDefault()
				});
			}
			return Unauthorized();
		}

		private JwtSecurityToken GetToken(List<Claim> authClaims)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

			var token = new JwtSecurityToken(
				issuer: _configuration["JWT:ValidIssuer"],
				audience: _configuration["JWT:ValidAudience"],
				expires: DateTime.Now.AddHours(3),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
				);

			return token;
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register([FromBody] ApplicationUser model)
		{
			var userExists = await _userManager.FindByNameAsync(model.UserName);
			if (userExists != null)
				return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });

			/*ApplicationUser user = new ApplicationUser()
			{
				UserName = model.Username,
				PasswordHash = model.Password,
				address = "addr"

			};*/
			try
			{
                await _userManager.CreateAsync(model, "P@55word");
				await _userManager.AddToRoleAsync(model, "THỦ THƯ");
                return Ok(new { Status = "Success", Message = "User created successfully!" });
            }
			catch {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }	
		}
	}
}
