using Events.EventsDbModels;
using System.Security.Claims;

namespace Events.JwtCreation
{
    public static class JwtClaims
    {
        public static List<Claim> CreateClaims(this UserDbModel user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            return claims;
        }
    }
}
