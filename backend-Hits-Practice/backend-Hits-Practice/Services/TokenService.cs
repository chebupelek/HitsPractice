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

    public async Task ValidateTokenAsync(string token, Guid userId)
    {
        var logDb = new LogDbModel
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            LogDate = DateTime.UtcNow,
            Token = token,
            IsLog = true
        };

        _logContext.Log.Add(logDb);
        await _logContext.SaveChangesAsync();
    }

    public async Task InvalidateTokenAsync(string token)
    {
        var bannedToken = await _logContext.Log.FirstOrDefaultAsync(x => x.Token == token);

        if (bannedToken == null)
        {
            throw new ArgumentException($"Токен '{token}' не найден.");
        }

        bannedToken.IsLog = false;
        _logContext.Update(bannedToken);
        _logContext.SaveChanges();
    }
}

