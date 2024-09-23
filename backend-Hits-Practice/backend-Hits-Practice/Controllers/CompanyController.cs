using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Events.Interfaces;
using Events.requestsModels;
using Events.responseModels;

namespace Events.Controllers;

[ApiController]
[Route("api/companies")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CompanyController(ICompanyService companyService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }


    [HttpGet]
    [Route("list")]
    [Authorize]
    public async Task<ActionResult> GetCompaniesList()
    {
        try
        {
            await _tokenService.BanningTokensAsync();

            var jwtToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                return Unauthorized();
            }

            var list = await _companyService.GetCompaniesListAsync(jwtToken);

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

    [HttpGet]
    [Route("names")]
    public async Task<ActionResult> GetCompaniesNames([FromQuery] string? name)
    {
        try
        {
            var list = await _companyService.GetCompaniesNamesAsync(name);

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
    public async Task<ActionResult> AddCompany([FromBody] CompanyCreateModel request)
    {
        try
        {
            await _tokenService.BanningTokensAsync();

            var jwtToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                return Unauthorized();
            }

            await _companyService.AddCompany(jwtToken, request);

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

    [HttpDelete]
    [Route("delete")]
    [Authorize]
    public async Task<ActionResult> DeleteCompany([FromBody] CompanyDeleteModel request)
    {
        Console.WriteLine("111111111111111111111111111111");
        try
        {
            await _tokenService.BanningTokensAsync();
            Console.WriteLine("222222222222222222222222222222222222");

            var jwtToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            Console.WriteLine(jwtToken);

            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                return Unauthorized();
            }
            Console.WriteLine("444444444444444444444444444444444444444444");

            await _companyService.DeleteCompany(jwtToken, request);
            Console.WriteLine("555555555555555555555555555555555");

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