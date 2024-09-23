using Events.innerModels;

namespace Events.Interfaces;

public interface IUserService
{
    Task<Guid> GetUserIdAsync(string token);
    Task<UserModel> GetProfileAsync(string jwtToken);
}