using System.ComponentModel.DataAnnotations;

namespace Events.EventsDbModels;

public class CompanyDbModel
{
    [Key]
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public required ICollection<CompanyRepresentativeDbModel> Employees { get; set; }
}
