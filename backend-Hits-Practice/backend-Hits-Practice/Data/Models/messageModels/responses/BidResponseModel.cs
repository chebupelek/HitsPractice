namespace Events.responseModels;

public class BidResponseModel
{
    public required Guid Id { get; set; }

    public required DateTime CreateTime { get; set; }

    public required string FullName { get; set; }

    public required string Email { get; set; }

    public required Guid CompanyId { get; set; }
    public required string Company { get; set; }
}
