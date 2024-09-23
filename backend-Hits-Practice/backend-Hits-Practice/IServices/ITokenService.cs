using Events.EventsDbModels;

namespace Events.Interfaces;

public interface ITokenService
{
    string CreateToken(UserDbModel doctor);
}