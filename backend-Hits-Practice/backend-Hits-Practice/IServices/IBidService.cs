using Events.innerModels;
using Events.requestsModels;
using Events.responseModels;

namespace Events.Interfaces;

public interface IBidService
{
    Task<bool> DoBidAsync(string token, AcceptBidModel acc);
    Task<BidsListResponseModel> GetBidsListAsync(string token);
}