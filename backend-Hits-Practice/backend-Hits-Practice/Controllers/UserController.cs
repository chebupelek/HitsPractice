using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Events.Interfaces;
using Events.requestsModels;
using Events.responseModels;
using Microsoft.AspNetCore.Authorization;

namespace Events.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserController(IUserService userService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }


    [HttpPost]
    [Route("registration/dean")]
    public async Task<ActionResult> DeanRegistration([FromBody] DeanRegistrationModel request)
    {
        try
        {
            await _tokenService.BanningTokensAsync();

            if ((!ModelState.IsValid) || (request == null))
            {
                return BadRequest();
            }

            var registrationResult = await _userService.RegisterDeanAsync(request);

            return Ok(new TokenResponseModel { Token = registrationResult });
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("registration/student")]
    public async Task<ActionResult> StudentRegistration([FromBody] StudentRegistrationModel request)
    {
        try
        {
            await _tokenService.BanningTokensAsync();

            if ((!ModelState.IsValid) || (request == null))
            {
                return BadRequest();
            }

            var registrationResult = await _userService.RegisterStudentAsync(request);

            return Ok(new TokenResponseModel { Token = registrationResult });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("registration/employee")]
    public async Task<ActionResult> EmployeeRegistration([FromBody] EmployeeBidModel request)
    {
        try
        {
            await _tokenService.BanningTokensAsync();

            if ((!ModelState.IsValid) || (request == null))
            {
                return BadRequest();
            }

            await _userService.CreateEmployeeBidAsync(request);

            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Authorization([FromBody] LoginCredentials request)
    {
        try
        {
            await _tokenService.BanningTokensAsync();

            if ((!ModelState.IsValid) || (request == null))
            {
                return BadRequest();
            }

            var authorizationResult = await _userService.AuthorizationAsync(request);

            return Ok(new TokenResponseModel { Token = authorizationResult });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("logout")]
    [Authorize]
    public async Task<ActionResult> Logout()
    {
        try
        {
            await _tokenService.BanningTokensAsync();

            var jwtToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                return Unauthorized();
            }

            await _userService.LogoutAsync(jwtToken);

            return Ok();
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}