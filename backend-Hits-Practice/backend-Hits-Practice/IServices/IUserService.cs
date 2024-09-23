using Events.innerModels;
using Events.requestsModels;

namespace Events.Interfaces;

public interface IUserService
{
    Task<Guid> GetUserIdAsync(string token);
    Task<UserModel> GetProfileAsync(string jwtToken);
    Task<string> RegisterDeanAsync(DeanRegistrationModel dean);
    Task<string> RegisterStudentAsync(StudentRegistrationModel student);
    Task<bool> CreateEmployeeBidAsync(EmployeeBidModel bid);
    Task<string> AuthorizationAsync(LoginCredentials loginData);
    Task LogoutAsync(string jwtToken);
}