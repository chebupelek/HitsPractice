using Events.innerModels;
using Events.requestsModels;

namespace Events.Interfaces;

public interface IUserService
{
    Task<string> RegisterDeanAsync(DeanRegistrationModel dean);
    Task<string> RegisterStudentAsync(StudentRegistrationModel student);
    Task<bool> CreateEmployeeBidAsync(EmployeeBidModel bid);
    Task<string> AuthorizationAsync(LoginCredentials loginData);
    Task LogoutAsync(string jwtToken);
    Task<UserModel> GetProfileAsync(string jwtToken);
    Task<Guid> GetUserIdAsync(string token);
}