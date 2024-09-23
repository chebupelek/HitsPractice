using System.ComponentModel.DataAnnotations;

namespace Events.EventsDbModels;

public class BidDbModel
{
    [Key]
    public required Guid Id { get; set; }

    public required DateTime CreateTime { get; set; }

    public required string FullName { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public required Guid CompanyId { get; set; }
    public required CompanyDbModel Company { get; set; }
}
