using Events.EventsDbModels;

namespace Events.Interfaces;

public interface ITokenService
{
    string CreateToken(UserDbModel doctor);
    Task ValidateTokenAsync(string token, Guid userId);
    Task InvalidateTokenAsync(string token);
    Task<bool> IsTokenValidAsync(string token);
}