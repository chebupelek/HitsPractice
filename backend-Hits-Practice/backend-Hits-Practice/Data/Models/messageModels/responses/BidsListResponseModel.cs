using Events.innerModels;

namespace Events.responseModels;

public class BidsListResponseModel
{
    public List<BidResponseModel>? BidsList { get; set; }

    public BidsListResponseModel(List<BidResponseModel>? bidsList)
    {
        this.BidsList = bidsList;
    }
}
