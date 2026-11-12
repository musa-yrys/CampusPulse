using CampusPulse.Data;
using CampusPulse.Models;
using CampusPulse.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusPulse.Controllers;

public class EventsController(CampusDbContext db) : Controller
{
    public async Task<IActionResult> Index(string? category, string? search)
    {
        var query = db.Events.AsNoTracking().Include(e => e.Registrations).Where(e => e.StartsAt >= DateTime.Now);
        if (!string.IsNullOrWhiteSpace(category)) query = query.Where(e => e.Category == category);
        if (!string.IsNullOrWhiteSpace(search)) query = query.Where(e => e.Title.Contains(search) || e.Club.Contains(search));
        ViewBag.Categories = await db.Events.Select(e => e.Category).Distinct().OrderBy(x => x).ToListAsync();
        ViewBag.Category = category;
        ViewBag.Search = search;
        return View(await query.OrderBy(e => e.StartsAt).ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var item = await db.Events.AsNoTracking().Include(e => e.Registrations).FirstOrDefaultAsync(e => e.Id == id);
        if (item is null) return NotFound();
        return View(new EventDetailsViewModel { Event = item, RegisteredCount = item.Registrations.Count(r => !r.IsWaitlisted) });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(int eventId, string studentName, string email)
    {
        var item = await db.Events.Include(e => e.Registrations).FirstOrDefaultAsync(e => e.Id == eventId);
        if (item is null) return NotFound();
        if (string.IsNullOrWhiteSpace(studentName) || string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
        {
            TempData["Notice"] = "Атыңыз бен жарамды email енгізіңіз.";
            return RedirectToAction(nameof(Details), new { id = eventId });
        }
        if (item.Registrations.Any(r => r.Email == email.Trim()))
        {
            TempData["Notice"] = "Бұл email осы іс-шараға тіркелген.";
            return RedirectToAction(nameof(Details), new { id = eventId });
        }
        var count = item.Registrations.Count(r => !r.IsWaitlisted);
        db.Registrations.Add(new EventRegistration
        {
            CampusEventId = eventId, StudentName = studentName.Trim(), Email = email.Trim(),
            IsWaitlisted = count >= item.Capacity
        });
        await db.SaveChangesAsync();
        TempData["Notice"] = count >= item.Capacity ? "Орын толды. Сіз күту тізіміне қосылдыңыз." : "Тіркелу сәтті өтті. Кездескенше!";
        return RedirectToAction(nameof(Details), new { id = eventId });
    }
}
