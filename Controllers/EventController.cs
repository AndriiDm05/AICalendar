using AICalendar.Data;
using AICalendar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AICalendar.Controllers;

[ApiController]
[Route("api/v1/events")]
public class EventsController : ControllerBase
{
    private readonly AppDbContext _context;

    public EventsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await _context.Events
            .Include(e => e.Participants)
            .ThenInclude(p => p.User)
            .ToListAsync();
        return Ok(events);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(Event newEvent)
    {
        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAllEvents), new { id = newEvent.Id }, newEvent);
    }
}
