using Events.innerModels;
using Events.requestsModels;
using Events.responseModels;

namespace Events.Interfaces;

public interface IEventService
{
    Task<EventsListResponseModel> GetEventsListAsync(string token, DateTime weekStart);
    Task<bool> AddEventAsync(string token, EventCreateModel eventData);
    Task<bool> ChangeEventAsync(string token, EventChangeModel eventData);
    Task<bool> DeleteEventAsync(string token, EventDeleteModel eventData);
    Task<bool> SignUpAsync(string token, SignUpEventModel eventData);
    Task<bool> UnSignUpAsync(string token, SignUpEventModel eventData);
}