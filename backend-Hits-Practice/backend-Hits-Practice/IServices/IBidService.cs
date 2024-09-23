using Events.innerModels;
using Events.requestsModels;
using Events.responseModels;

namespace Events.Interfaces;

public interface IBidService
{
    Task<BidsListResponseModel> GetBidsListAsync(string token);
    Task<bool> DoBidAsync(string token, AcceptBidModel acc);
}