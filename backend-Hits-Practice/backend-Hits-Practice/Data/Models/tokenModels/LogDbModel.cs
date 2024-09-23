using System.ComponentModel.DataAnnotations;

namespace Events.TokenDbModels;

public class LogDbModel
{
    [Key]
    public required Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required DateTime LogDate { get; set; }
    public required string Token { get; set; }
    public required bool IsLog { get; set; }
}
