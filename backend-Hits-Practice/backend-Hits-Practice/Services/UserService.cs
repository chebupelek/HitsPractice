using Events.Contexts;
using Events.EventsDbModels;
using Events.innerModels;
using Events.Interfaces;
using Events.requestsModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Events.Services;

public class UserService: IUserService
{
    private readonly EventsContext _eventsContext;
    private readonly ITokenService _tokenService;
    private readonly PasswordHasher<string> _passwordHasher;

    public UserService(EventsContext eventsContext, ITokenService tokenService)
    {
        _eventsContext = eventsContext ?? throw new ArgumentNullException(nameof(eventsContext));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _passwordHasher = new PasswordHasher<string>();
    }


    public async Task<string> RegisterDeanAsync(DeanRegistrationModel dean)
    {
        var existingdean = await _eventsContext.Users.FirstOrDefaultAsync(d => d.Email == dean.Email);
        if (existingdean != null)
        {
            throw new ArgumentException("A dean with this email already exists");
        }

        var newDean = new DeanDbModel
        {
            Id = Guid.NewGuid(),
            FullName = dean.FullName,
            Email = dean.Email,
            Password = _passwordHasher.HashPassword(dean.Email, dean.Password)
        };

        _eventsContext.Deans.Add(newDean);
        await _eventsContext.SaveChangesAsync();

        var token = _tokenService.CreateToken(newDean);
        await _tokenService.ValidateTokenAsync(token, newDean.Id);

        return token;
    }

    public async Task<string> RegisterStudentAsync(StudentRegistrationModel student)
    {
        var existingstudent = await _eventsContext.Users.FirstOrDefaultAsync(d => d.Email == student.Email);
        if (existingstudent != null)
        {
            throw new ArgumentException("A student with this email already exists");
        }

        var newStudent = new StudentDbModel
        {
            Id = Guid.NewGuid(),
            FullName = student.FullName,
            Email = student.Email,
            Password = _passwordHasher.HashPassword(student.Email, student.Password),
            GroupNumber = student.Group,
            Events = new List<EventDbModel>()
        };

        _eventsContext.Students.Add(newStudent);
        await _eventsContext.SaveChangesAsync();

        var token = _tokenService.CreateToken(newStudent);
        await _tokenService.ValidateTokenAsync(token, newStudent.Id);

        return token;
    }

    public async Task<bool> CreateEmployeeBidAsync(EmployeeBidModel bid)
    {
        var existingemployee = await _eventsContext.Users.FirstOrDefaultAsync(d => d.Email == bid.Email);
        if (existingemployee != null)
        {
            throw new ArgumentException("A employee with this email already exists");
        }

        var existingbid = await _eventsContext.Bids.FirstOrDefaultAsync(d => d.Email == bid.Email);
        if (existingbid != null)
        {
            throw new ArgumentException("A bid with this email already exists");
        }

        var company = await _eventsContext.Companies.FirstOrDefaultAsync(com => com.Id == bid.CompanyId);
        if (company == null)
        {
            throw new ArgumentException("A company with this id doesnt exists");
        }

        var newBid = new BidDbModel
        {
            Id = Guid.NewGuid(),
            FullName = bid.FullName,
            Email = bid.Email,
            Password = _passwordHasher.HashPassword(bid.Email, bid.Password),
            CreateTime = DateTime.Now.ToUniversalTime(),
            CompanyId = bid.CompanyId,
            Company = company
        };

        _eventsContext.Bids.Add(newBid);
        await _eventsContext.SaveChangesAsync();

        return true;
    }


    public async Task<string> AuthorizationAsync(LoginCredentials loginData)
    {
        var existinguser = await _eventsContext.Users.FirstOrDefaultAsync(u => u.Email == loginData.Email);
        if (existinguser == null)
        {
            throw new ArgumentException("A user with this email doesnt exists");
        }

        var logresult = _passwordHasher.VerifyHashedPassword(loginData.Email, existinguser.Password, loginData.Password);

        if (logresult == PasswordVerificationResult.Failed)
        {
            throw new ArgumentException("Wrong password");
        }

        var token = _tokenService.CreateToken(existinguser);
        await _tokenService.ValidateTokenAsync(token, existinguser.Id);

        return token;
    }


    public async Task LogoutAsync(string jwtToken)
    {
        var tokenCheck = await _tokenService.IsTokenValidAsync(jwtToken);
        if (!tokenCheck)
        {
            throw new UnauthorizedAccessException();
        }
        await _tokenService.InvalidateTokenAsync(jwtToken);
    }


    public async Task<UserModel> GetProfileAsync(string jwtToken)
    {
        var userId = await GetUserIdAsync(jwtToken);

        var user = await _eventsContext.Users
            .Where(u => u.Id == userId)
            .Select(u => new
            {
                u.Id,
                u.FullName,
                u.Email,
                u.GetType().Name
            })
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new KeyNotFoundException("User не найден");
        }

        var userRole = user switch
        {
            { Name: nameof(StudentDbModel) } => RoleEnum.Student,
            { Name: nameof(DeanDbModel) } => RoleEnum.Dean,
            { Name: nameof(CompanyRepresentativeDbModel) } => RoleEnum.Employee,
            _ => RoleEnum.Undefined
        };

        if (userRole == RoleEnum.Undefined) { throw new KeyNotFoundException("Role не найден"); }

        var searchedData = new UserModel
        {
            id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = userRole
        };

        return searchedData;
    }

    public async Task<Guid> GetUserIdAsync(string token)
    {
        var tokenCheck = await _tokenService.IsTokenValidAsync(token);
        if (!tokenCheck)
        {
            throw new UnauthorizedAccessException();
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
        var userId = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            throw new KeyNotFoundException("User не найден");
        }
        Guid userIdGuid = Guid.Parse(userId);

        return userIdGuid;
    }
}