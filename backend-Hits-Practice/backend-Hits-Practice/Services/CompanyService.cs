using Events.Contexts;
using Events.EventsDbModels;
using Events.innerModels;
using Events.Interfaces;
using Events.requestsModels;
using Events.responseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Events.Services;

public class CompanyService: ICompanyService
{
    private readonly EventsContext _eventsContext;
    private readonly IUserService _userService;

    public CompanyService(EventsContext eventsContext, IUserService userService)
    {
        _eventsContext = eventsContext ?? throw new ArgumentNullException(nameof(eventsContext));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }


    public async Task<CompaniesListResponseModel> GetCompaniesListAsync(string token)
    {
        var userData = await _userService.GetProfileAsync(token);
        if(userData == null || userData.Role != RoleEnum.Dean) { throw new ArgumentException(); }

        var companiesList = new CompaniesListResponseModel
        {
            Companies = await _eventsContext.Companies.Select(
                c => new CompanyResponseModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email
                }
            ).ToListAsync()
        };

        return companiesList;
    }

    public async Task<CompaniesNamesResponseModel> GetCompaniesNamesAsync(string name)
    {
        var companiesList = new CompaniesNamesResponseModel
        {
            Companies = await _eventsContext.Companies
                .Where(c => string.IsNullOrEmpty(name) || c.Name.Contains(name))
                .Select(c => new CompanyNameResponseModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync()
                };

        return companiesList;
    }

    public async Task<bool> AddCompany(string token, CompanyCreateModel company)
    {
        var userData = await _userService.GetProfileAsync(token);
        Console.WriteLine(userData);
        if (userData == null || userData.Role != RoleEnum.Dean) { throw new ArgumentException(); }
        var checkCompany = await _eventsContext.Companies.FirstOrDefaultAsync(c => c.Email == company.Email || c.Name == company.Name);
        Console.WriteLine(checkCompany);
        if (checkCompany != null) { throw new ArgumentException(); }

        var newCompany = new CompanyDbModel
        {
            Id = Guid.NewGuid(),
            Name = company.Name,
            Email = company.Email,
            Employees = new List<CompanyRepresentativeDbModel>()
        };
        Console.WriteLine(newCompany);

        _eventsContext.Companies.Add(newCompany);
        await _eventsContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteCompany(string token, CompanyDeleteModel company)
    {
        var userData = await _userService.GetProfileAsync(token);
        if (userData == null || userData.Role != RoleEnum.Dean) { throw new ArgumentException(); }

        var deleteCompany = await _eventsContext.Companies.FirstOrDefaultAsync(c => c.Id == company.id);
        if (deleteCompany == null) { throw new ArgumentException(); }

        _eventsContext.Remove(deleteCompany);
        await _eventsContext.SaveChangesAsync();

        return true;
    }
}