using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SkillSprint.Data;
using SkillSprint.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SkillSprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SkillSprintContext _context;
        private readonly IPasswordHasher<RegisterUserDTO> _passwordHasher;

        public LoginController(IConfiguration configuration, SkillSprintContext context, IPasswordHasher<RegisterUserDTO> passHash)
        {
            _configuration = configuration;
            _context = context;
            _passwordHasher = passHash;
        }

        [HttpPost]
        public IActionResult Login(LoginDTO userCreds)
        {
            // look for user
            SkillSprint.Models.User userToLogin = _context.Users.FirstOrDefault(u => u.UserName.ToLower() == userCreds.username.ToLower());
            if (userToLogin == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }
            // Verify the password against the hash
            PasswordVerificationResult verificationResult =
                _passwordHasher.VerifyHashedPassword(new RegisterUserDTO(), userToLogin.PasswordHash, userCreds.password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }
            // Return the user object with a token
            string role = "User";
            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, userCreds.username)
            };

            // need to add the key to our token
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
                );
            string tokenToReturn = new JwtSecurityTokenHandler().WriteToken(token);
            userToLogin.token = tokenToReturn;
            return Ok(userToLogin);
        }

        [HttpPost("/register")]
        public IActionResult RegisterUser(RegisterUserDTO registerUserDTO)
        {
            //check if user exists
            SkillSprint.Models.User existingUser = _context.Users.FirstOrDefault(u => u.UserName == registerUserDTO.UserName);
            if (existingUser != null)
            {
                return Conflict("Cannot register user.");
            }
            // hash the password
            string hashedPassword = _passwordHasher.HashPassword(registerUserDTO, registerUserDTO.Password);
            // create a new user object
            SkillSprint.Models.User newUser = new User
            {
                UserName = registerUserDTO.UserName,
                PasswordHash = hashedPassword,
                Email = registerUserDTO.Email,
                FirstName = registerUserDTO.FirstName,
                LastName = registerUserDTO.LastName,
                isActive = true
            };
            try
            {
                _context.Users.Add(newUser);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Could not register user" });
            }
            // return 200 if all goes well
            return Ok();
        }
    }
}
