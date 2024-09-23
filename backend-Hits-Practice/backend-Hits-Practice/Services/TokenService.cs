using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Events.Interfaces;
using Events.EventsDbModels;
using Events.JwtCreation;
using Events.Contexts;
using Events.TokenDbModels;

namespace Events.Services;

public class TokenService: ITokenService
{
    private readonly IConfiguration _configuration;
    private LogContext _logContext;

    public TokenService(LogContext tokenContext, IConfiguration configuration)
    {
        _logContext = tokenContext ?? throw new ArgumentNullException(nameof(tokenContext));
        _configuration = configuration;
    }

    public string CreateToken(UserDbModel user)
    {
        var token = user.CreateClaims().CreateJwtToken(_configuration);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}

