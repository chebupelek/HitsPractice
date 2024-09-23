using Microsoft.Extensions.Logging;

namespace Events.EventsDbModels;

public class StudentDbModel : UserDbModel
{
    public required string GroupNumber { get; set; }

    public required ICollection<EventDbModel> Events { get; set; }
}
