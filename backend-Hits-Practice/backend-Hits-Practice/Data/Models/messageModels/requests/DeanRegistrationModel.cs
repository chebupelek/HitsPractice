using System.ComponentModel.DataAnnotations;

namespace Events.requestsModels;

public class DeanRegistrationModel
{
    [Required(ErrorMessage = "Full name is required.")]
    [MinLength(1, ErrorMessage = "Full name must have at least 1 character.")]
    [MaxLength(1000, ErrorMessage = "Full name cannot exceed 1000 characters.")]
    public required string FullName { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must have at least 6 characters.")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [MinLength(1, ErrorMessage = "Email must have at least 1 character.")]
    public required string Email { get; set; }
}
