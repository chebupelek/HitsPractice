using System.ComponentModel.DataAnnotations;

namespace Events.requestsModels;

public class CompanyCreateModel
{
    [Required(ErrorMessage = "Name is required.")]
    [MinLength(1, ErrorMessage = "Name must have at least 1 character.")]
    [MaxLength(1000, ErrorMessage = "Name cannot exceed 1000 characters.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [MinLength(1, ErrorMessage = "Email must have at least 1 character.")]
    public required string Email { get; set; }
}
