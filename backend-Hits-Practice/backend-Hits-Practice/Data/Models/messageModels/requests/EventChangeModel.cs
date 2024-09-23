using System.ComponentModel.DataAnnotations;

namespace Events.requestsModels;

public class EventChangeModel
{
    [Required(ErrorMessage = "Id is required.")]
    public required Guid Id { get; set; }
    public string? name { get; set; }
    public string? description { get; set; }
    public string? location { get; set; }
}
