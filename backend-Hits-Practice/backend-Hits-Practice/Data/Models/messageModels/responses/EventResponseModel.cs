using Events.EventsDbModels;

namespace Events.responseModels;

public class EventResponseModel
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public required DateTime CreatedDate { get; set; }

    public required DateTime EventDate { get; set; }

    public required string Location { get; set; }

    public DateTime? Deadline { get; set; }

    public required string Employee { get; set; }

    public required Guid EmployeeId { get; set; }

    public bool? isSign {  get; set; }

    public bool? isCreate { get; set; }
}
