using System.ComponentModel.DataAnnotations;

namespace Events.EventsDbModels;

public class EventDbModel
{
    [Key]
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public required DateTime CreatedDate { get; set; }

    public required DateTime EventDate { get; set; }

    public required string Location { get; set; }

    public DateTime? Deadline { get; set; }

    public required Guid EmployeeId { get; set; }
    public required CompanyRepresentativeDbModel Employee { get; set; }

    public required ICollection<StudentDbModel> Students { get; set; }
}
