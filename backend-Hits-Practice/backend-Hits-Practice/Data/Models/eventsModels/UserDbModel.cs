using System.ComponentModel.DataAnnotations;

namespace Events.EventsDbModels;

public class UserDbModel
{
    [Key]
    public required Guid Id { get; set; }

    public required string FullName { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}
