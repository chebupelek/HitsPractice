using System.ComponentModel.DataAnnotations;

namespace Events.requestsModels;

public class EventDeleteModel
{
    [Required(ErrorMessage = "Id is required.")]
    public required Guid id { get; set; }
}
