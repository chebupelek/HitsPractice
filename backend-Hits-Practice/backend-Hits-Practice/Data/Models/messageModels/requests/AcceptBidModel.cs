using System.ComponentModel.DataAnnotations;

namespace Events.requestsModels;

public class AcceptBidModel
{
    [Required(ErrorMessage = "Bid id is required.")]
    public required Guid BidId { get; set; }

    [Required(ErrorMessage = "IsAccepted is required.")]
    public required bool IsAccepted { get; set; }
}
