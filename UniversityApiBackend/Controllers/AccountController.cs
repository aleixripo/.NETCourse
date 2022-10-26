using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UniversityDBContext _dbContext;

        private readonly JwtSettings _jwtSettings;

        public AccountController(JwtSettings jwtSettings, UniversityDBContext dBContext)
        {
            _jwtSettings = jwtSettings;
            _dbContext = dBContext;
        }

        private IEnumerable<User> Logins()
        {
            return _dbContext.Users.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> GetTokenAsync(UserLogins userLogin)
        {
            try
            {
                var Token = new UserTokens();

                List<User> listUsers = new List<User>(Logins());

                var Valid = listUsers.Any(user => user.Email.Equals(userLogin.Email, StringComparison.OrdinalIgnoreCase));

                if (Valid)
                {
                    var user = listUsers.FirstOrDefault(user => user.Email.Equals(userLogin.Email, StringComparison.OrdinalIgnoreCase));

                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidID = Guid.NewGuid()
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }

                return Ok(Token);

            }
            catch (Exception e)
            {
                throw new Exception("GetToken Error", e);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUserList()
        {
            return Ok(_dbContext.Users);
        }
    }
}
