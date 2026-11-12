using CampusPulse.Data;
using CampusPulse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusPulse.Controllers;

public class ManageController(CampusDbContext db) : Controller
{
    public async Task<IActionResult> Index() => View(await db.Events.AsNoTracking()
        .Include(e => e.Registrations).OrderByDescending(e => e.StartsAt).ToListAsync());

    public IActionResult Create() => View(new CampusEvent { StartsAt = DateTime.Now.AddDays(7) });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CampusEvent item)
    {
        if (!ModelState.IsValid) return View(item);
        db.Events.Add(item);
        await db.SaveChangesAsync();
        TempData["Notice"] = "Іс-шара жарияланды.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await db.Events.FindAsync(id);
        if (item is null) return NotFound();
        db.Events.Remove(item);
        await db.SaveChangesAsync();
        TempData["Notice"] = "Іс-шара өшірілді.";
        return RedirectToAction(nameof(Index));
    }
}
