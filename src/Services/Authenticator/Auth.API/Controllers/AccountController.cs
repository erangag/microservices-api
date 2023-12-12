using Auth.Infrastructure.Models;
using Auth.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAutheManager _authManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAutheManager authManager, ILogger<AccountController> logger)
        {
            this._authManager = authManager;
            this._logger = logger;
        }

        //api/Account/login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Login([FromBody] LoginDto userDto)
        {
            _logger.LogInformation($"Login Attempt for {userDto.Email}");

            try
            {
                var authResponse = await _authManager.Login(userDto);

                if (authResponse == null)
                {
                    return Unauthorized();
                }

                return Ok(authResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(Login)} - User Registration attemtp for {userDto.Email}");
                return Problem($"Something Went Wrong in the {nameof(Login)}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        //api/Account/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Register([FromBody] ApiUserDto userDto)
        {
            _logger.LogInformation($"Registration Attempt for {userDto.Email}");

            try
            {
                var errors = await _authManager.Register(userDto);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    return BadRequest(ModelState);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(Register)} - User Registration attemtp for {userDto.Email}");
                return Problem($"Something Went Wrong in the {nameof(Register)}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Account/refreshtoken
        [HttpPost]
        [Route("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDto request)
        {
            var authResponse = await _authManager.VerifyRefreshToken(request);

            if (authResponse == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }
    }
}
}
