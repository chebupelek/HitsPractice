﻿using Events.Contexts;
using Events.EventsDbModels;
using Events.innerModels;
using Events.Interfaces;
using Events.requestsModels;
using Events.responseModels;
using Microsoft.EntityFrameworkCore;

namespace Events.Services;

public class EventService: IEventService
{
    private readonly EventsContext _eventsContext;
    private readonly IUserService _userService;

    public EventService(EventsContext eventsContext, IUserService userService)
    {
        _eventsContext = eventsContext ?? throw new ArgumentNullException(nameof(eventsContext));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }


    public async Task<EventsListResponseModel> GetEventsListAsync(string token, DateTime weekStart)
    {
        var userData = await _userService.GetProfileAsync(token);
        if (userData == null) { throw new ArgumentException(); }

        var weekEnd = weekStart.AddDays(7);
        var eventsList = new List<EventResponseModel>();

        if (userData.Role == RoleEnum.Student)
        {
            eventsList = await _eventsContext.Events
                .Where(e => e.EventDate >= weekStart && e.EventDate < weekEnd)
                .Include(e => e.Employee)
                .GroupJoin(
                    _eventsContext.Students
                        .Where(s => s.Id == userData.id)
                        .SelectMany(s => s.Events),
                    e => e.Id,
                    se => se.Id,
                    (e, studentEvents) => new EventResponseModel
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Description = e.Description,
                        CreatedDate = e.CreatedDate,
                        EventDate = e.EventDate,
                        Location = e.Location,
                        Deadline = e.Deadline,
                        Employee = e.Employee.FullName,
                        EmployeeId = e.EmployeeId,
                        isCreate = null,
                        isSign = studentEvents.Any()
                    })
                .ToListAsync();
        } else if ( userData.Role == RoleEnum.Employee)
        {
            eventsList = await _eventsContext.Employees
                .Where(e => e.Id == userData.id)
                .SelectMany(e => _eventsContext.Events
                    .Where(ev => ev.EventDate >= weekStart && ev.EventDate < weekEnd && ev.Employee.CompanyId == e.CompanyId)
                    .Select(ev => new EventResponseModel
                    {
                        Id = ev.Id,
                        Name = ev.Name,
                        Description = ev.Description,
                        CreatedDate = ev.CreatedDate,
                        EventDate = ev.EventDate,
                        Location = ev.Location,
                        Deadline = ev.Deadline,
                        Employee = ev.Employee.FullName,
                        EmployeeId = ev.EmployeeId,
                        isCreate = ev.EmployeeId == e.Id,
                        isSign = null
                    }))
                    .ToListAsync();
        } else if (userData.Role == RoleEnum.Dean)
        {
            eventsList = await _eventsContext.Events
                .Where(e => e.EventDate >= weekStart && e.EventDate < weekEnd)
                .Include(e => e.Employee)
                .Select(e => new EventResponseModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    CreatedDate = e.CreatedDate,
                    EventDate = e.EventDate,
                    Location = e.Location,
                    Deadline = e.Deadline,
                    Employee = e.Employee.FullName,
                    EmployeeId = e.EmployeeId,
                    isCreate = null,
                    isSign = null
                })
                .ToListAsync();
        }
        else
        {
            throw new KeyNotFoundException("Роль не поддерживается");
        }

        var answer = new EventsListResponseModel
        {
            Events = CreateWeekListAsync(eventsList, weekStart)
        };

        return answer;
    }

    public List<List<EventResponseModel>> CreateWeekListAsync(List<EventResponseModel> events, DateTime weekStart)
    {
        var weekList = new List<List<EventResponseModel>>();

        for (int i = 0; i < 7; i++)
        {
            var currentDay = weekStart.AddDays(i);
            var eventsForDay = events
                .Where(e => e.EventDate.Date == currentDay.Date)
                .ToList();

            weekList.Add(eventsForDay);
        }

        return weekList;
    }

    public async Task<bool> AddEventAsync(string token, EventCreateModel eventData)
    {
        var userData = await _userService.GetProfileAsync(token);
        if (userData == null || userData.Role != RoleEnum.Employee) { throw new KeyNotFoundException(); }

        if (eventData.EventDate <= DateTime.Now.ToUniversalTime().AddDays(7)) { throw new ArgumentException(); }

        if (eventData.Deadline != null && eventData.Deadline >= eventData.EventDate) { throw new ArgumentException(); }

        var employee = await _eventsContext.Employees.FirstOrDefaultAsync(e => e.Id == userData.id);
        if (employee == null) { throw new KeyNotFoundException(); }

        var newEvent = new EventDbModel
        {
            Id = Guid.NewGuid(),
            Name = eventData.Name,
            Description = eventData.Description,
            CreatedDate = DateTime.Now.ToUniversalTime(),
            EventDate = eventData.EventDate,
            Location = eventData.Location,
            Deadline = eventData.Deadline,
            EmployeeId = employee.Id,
            Employee = employee,
            Students = new List<StudentDbModel>()
        };

        _eventsContext.Events.Add(newEvent);
        await _eventsContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ChangeEventAsync(string token, EventChangeModel eventData)
    {
        var userData = await _userService.GetProfileAsync(token);
        if (userData == null || userData.Role != RoleEnum.Employee) { throw new KeyNotFoundException("1"); }

        var changebleEvent = await _eventsContext.Events.FirstOrDefaultAsync(e => e.Id == eventData.Id);
        if (changebleEvent == null) { throw new KeyNotFoundException("2"); }

        if (changebleEvent.EmployeeId != userData.id ||
            changebleEvent.EventDate <= DateTime.Now.ToUniversalTime().AddDays(1)) { throw new ArgumentException("3"); }

        if (eventData.name != null) { changebleEvent.Name = eventData.name; }
        if (eventData.description != null) { changebleEvent.Description = eventData.description; }
        if (eventData.location != null) { changebleEvent.Location = eventData.location; }

        _eventsContext.Events.Update(changebleEvent);
        await _eventsContext.SaveChangesAsync();

        return true;
    }
}