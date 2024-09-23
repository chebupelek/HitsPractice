using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Events.Interfaces;
using Events.requestsModels;
using Events.responseModels;

namespace Events.Controllers;

[ApiController]
[Route("api/bids")]
public class BidController : ControllerBase
{
    private readonly IBidService _bidService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BidController(IBidService bidService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _bidService = bidService ?? throw new ArgumentNullException(nameof(bidService));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }


    [HttpPut]
    [Route("access")]
    [Authorize]
    public async Task<ActionResult> AccessBid([FromBody] AcceptBidModel request)
    {
        try
        {
            await _tokenService.BanningTokensAsync();

            var jwtToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                return Unauthorized();
            }

            await _bidService.DoBidAsync(jwtToken, request);

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

    [HttpGet]
    [Route("getList")]
    [Authorize]
    public async Task<ActionResult> GetBidsList()
    {
        try
        {
            await _tokenService.BanningTokensAsync();

            var jwtToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                return Unauthorized();
            }

            var list = await _bidService.GetBidsListAsync(jwtToken);

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
}