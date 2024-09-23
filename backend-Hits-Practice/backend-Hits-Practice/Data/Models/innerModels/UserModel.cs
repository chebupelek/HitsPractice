namespace Events.innerModels;

public class UserModel
{
    public required Guid id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required RoleEnum Role { get; set; }
}