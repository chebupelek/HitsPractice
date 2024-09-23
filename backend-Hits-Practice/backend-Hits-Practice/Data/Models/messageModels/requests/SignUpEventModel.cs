using System.ComponentModel.DataAnnotations;

namespace Events.requestsModels;

public class SignUpEventModel
{
    [Required(ErrorMessage = "Id is required.")]
    public required Guid id { get; set; }
}
