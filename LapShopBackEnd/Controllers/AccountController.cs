using BuisnessLibrary.Bl.Account;
using BuisnessLibrary.Dto.Account;
using BuisnessLibrary.Dto.Item;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace LapShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userMangerService, IConfiguration configuration)
        {
            _userManger = userMangerService;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterUserDto registerUserDto)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    var result =
                        await _userManger
                        .CreateAsync(AccountMapper.ConvertDto(registerUserDto)
                        , registerUserDto.Password);

                    if (result.Succeeded)
                    {

                        var response = new ApiResponse("New Account was created", ResponseStatus.Success);
                        return Ok(response);

                    }
                    else
                    {
                        var badErrors = result.Errors.Select(v => new {
                         v.Code,
                         v.Description
                        }).ToList();

                        return BadRequest(new ApiResponse(badErrors, ResponseStatus.NotValid));

                    }


                }


                // If model state is invalid, return the errors in the response
                var errors = ModelState.Values
                               .SelectMany(v => v.Errors)
                               .Select(e => e.ErrorMessage)
                               .ToList();


                return BadRequest(new ApiResponse(registerUserDto, ResponseStatus.NotValid)
                {
                    Errors = errors// Pass the extracted errors to the Errors property
                });
            }
            catch (Exception ex)
            {
                // Log the exception 

                var errorResponse = new ApiResponse(null, ResponseStatus.Error)
                {
                    Errors = new List<string> { ex.Message }
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);

            }

        }


        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser applicationUser = await GetUserAsync(loginUserDto.UserName);
           
            if (applicationUser == null)
            {
                return Unauthorized(new ApiResponse(loginUserDto, ResponseStatus.Unauthorized));
            }

            if (!await IsPasswordValidAsync(applicationUser, loginUserDto.Password))
            {
                return Unauthorized(new ApiResponse(loginUserDto, ResponseStatus.Unauthorized));
            }

            var token = await GenerateJwtTokenAsync(applicationUser);
            return Ok(new { Token = token });

        }

        private async Task<ApplicationUser> GetUserAsync(string username)
        {
            return await _userManger.FindByNameAsync(username);
        }

        private async Task<bool> IsPasswordValidAsync(ApplicationUser applicationUser, string password)
        {
            return await _userManger.CheckPasswordAsync(applicationUser, password);
        }

        private async Task<string> GenerateJwtTokenAsync(ApplicationUser applicationUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, applicationUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roles = await _userManger.GetRolesAsync(applicationUser);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.Aes128CbcHmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        }

    }

}
