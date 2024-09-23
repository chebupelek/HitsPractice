using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Events.Interfaces;
using Events.requestsModels;
using Events.responseModels;

namespace Events.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EventsController(IEventService eventService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }


    [HttpGet]
    [Route("list")]
    [Authorize]
    public async Task<ActionResult> GetEventsList([FromQuery] DateTime weekStart)
    {
        try
        {
            await _tokenService.BanningTokensAsync();

            var jwtToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                return Unauthorized();
            }

            var list = await _eventService.GetEventsListAsync(jwtToken, weekStart);

            return Ok(list);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized();
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
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
    [Route("create")]
    [Authorize]
    public async Task<ActionResult> AddEvent([FromBody] EventCreateModel request)
    {
        try
        {
            await _tokenService.BanningTokensAsync();

            var jwtToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                return Unauthorized();
            }

            await _eventService.AddEventAsync(jwtToken, request);

            return Ok();
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized();
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
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