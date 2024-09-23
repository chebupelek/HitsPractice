using System.ComponentModel.DataAnnotations;

namespace Events.requestsModels;

public class CompanyDeleteModel
{
    [Required(ErrorMessage = "Id is required.")]
    public required Guid id { get; set; }
}
