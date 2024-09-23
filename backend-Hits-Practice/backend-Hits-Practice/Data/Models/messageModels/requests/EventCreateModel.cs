using Events.EventsDbModels;
using System.ComponentModel.DataAnnotations;

namespace Events.requestsModels;

public class EventCreateModel
{
    [Required(ErrorMessage = "Name is required.")]
    [MinLength(1, ErrorMessage = "Name must have at least 1 character.")]
    [MaxLength(1000, ErrorMessage = "Name cannot exceed 1000 characters.")]
    public required string Name { get; set; }


    public string? Description { get; set; }

    [Required(ErrorMessage = "EventDate is required.")]
    public required DateTime EventDate { get; set; }

    [Required(ErrorMessage = "Location is required.")]
    [MinLength(1, ErrorMessage = "Location must have at least 1 character.")]
    [MaxLength(1000, ErrorMessage = "Location cannot exceed 1000 characters.")]
    public required string Location { get; set; }

    public DateTime? Deadline { get; set; }
}
