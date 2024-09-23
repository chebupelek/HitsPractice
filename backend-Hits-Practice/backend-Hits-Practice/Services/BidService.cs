using Events.Contexts;
using Events.EventsDbModels;
using Events.innerModels;
using Events.Interfaces;
using Events.requestsModels;
using Events.responseModels;
using Microsoft.EntityFrameworkCore;

namespace Events.Services;

public class BidService: IBidService
{
    private readonly EventsContext _eventsContext;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public BidService(EventsContext eventsContext, IUserService userService, ITokenService tokenService)
    {
        _eventsContext = eventsContext ?? throw new ArgumentNullException(nameof(eventsContext));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task<BidsListResponseModel> GetBidsListAsync(string token)
    {
        var userData = await _userService.GetProfileAsync(token);
        if (userData == null) { throw new KeyNotFoundException("User не найден"); }

        if (userData.Role == RoleEnum.Dean)
        {
            var bids = new BidsListResponseModel(await _eventsContext.Bids.Select(b => new BidResponseModel
            {
                Id = b.Id,
                CreateTime = b.CreateTime,
                FullName = b.FullName,
                Email = b.Email,
                CompanyId = b.CompanyId,
                Company = b.Company.Name
            }).ToListAsync());
            return bids;
        }

        if (userData.Role == RoleEnum.Employee)
        {
            var bids = new BidsListResponseModel(await _eventsContext.Bids
                .Where(b => b.Company.Employees.Any(e => e.Id == userData.id))
                .Select(b => new BidResponseModel
                {
                    Id = b.Id,
                    CreateTime = b.CreateTime,
                    FullName = b.FullName,
                    Email = b.Email,
                    CompanyId = b.CompanyId,
                    Company = b.Company.Name
                }).ToListAsync());
            return bids;
        }
        else
        {
            throw new ArgumentException(userData.FullName + " " + userData.Email + " " + userData.id.ToString() + " " + userData.Role.ToString());
        }
    }


    public async Task<bool> DoBidAsync(string token, AcceptBidModel acc)
    {
        var userId = await _userService.GetUserIdAsync(token);

        var user = await _eventsContext.Users
            .Where(u => u.Id == userId)
            .Select(u => new
            {
                u.Id,
                u.GetType().Name
            })
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new KeyNotFoundException("User не найден");
        }

        var bid = await _eventsContext.Bids.FirstOrDefaultAsync(b => b.Id == acc.BidId);

        if (bid == null)
        {
            throw new KeyNotFoundException("Bid не найден");
        }

        switch (user.Name)
        {
            case nameof(StudentDbModel):
                throw new ArgumentException("Wrong role");

            case nameof(DeanDbModel):
                return await AcceptBidAsync(acc);

            case nameof(CompanyRepresentativeDbModel):
                var employee = await _eventsContext.Employees.FirstOrDefaultAsync(e => e.Id == user.Id);
                if (employee == null) { throw new KeyNotFoundException("User не найден"); }
                if (employee.CompanyId != bid.CompanyId) { throw new ArgumentException("Wrong company"); }
                return await AcceptBidAsync(acc);

            default:
                throw new KeyNotFoundException("Unknown user role");
        }
    }

    public async Task<bool> AcceptBidAsync(AcceptBidModel acc)
    {
        var bid = await _eventsContext.Bids.FirstOrDefaultAsync(b => b.Id == acc.BidId);

        if (bid == null)
        {
            throw new KeyNotFoundException("Bid не найден");
        }

        if (acc.IsAccepted == false)
        {
            _eventsContext.Bids.Remove(bid);
            await _eventsContext.SaveChangesAsync();
        }
        else
        {
            var company = await _eventsContext.Companies.FirstOrDefaultAsync(c => c.Id == bid.CompanyId);
            if (company == null)
            {
                throw new KeyNotFoundException("Company не найден");
            }

            var newEmployee = new CompanyRepresentativeDbModel
            {
                Id = Guid.NewGuid(),
                FullName = bid.FullName,
                Email = bid.Email,
                Password = bid.Password,
                CompanyId = company.Id,
                Company = company
            };

            _eventsContext.Bids.Remove(bid);
            _eventsContext.Employees.Add(newEmployee);
            await _eventsContext.SaveChangesAsync();
        }

        return true;
    }
}