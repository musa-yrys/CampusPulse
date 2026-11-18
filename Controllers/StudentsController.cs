using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models;

namespace StudentRegistrationSystem.Controllers;

public class StudentsController(StudentDbContext context) : Controller
{
    public async Task<IActionResult> Index(string? searchString)
    {
        var students = context.Students
            .AsNoTracking()
            .Include(student => student.Group)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            var term = searchString.Trim();
            students = students.Where(student =>
                student.FirstName.Contains(term) || student.LastName.Contains(term));
        }

        ViewData["SearchString"] = searchString;
        return View(await students.OrderBy(student => student.LastName)
            .ThenBy(student => student.FirstName).ToListAsync());
    }

    public async Task<IActionResult> Create()
    {
        await LoadGroupsAsync();
        return View(new Student());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Phone,GroupId")] Student student)
    {
        if (!await context.Groups.AnyAsync(group => group.Id == student.GroupId))
            ModelState.AddModelError(nameof(student.GroupId), "Таңдалған топ табылмады.");

        if (ModelState.IsValid)
        {
            context.Students.Add(student);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        await LoadGroupsAsync(student.GroupId);
        return View(student);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var student = await context.Students.FindAsync(id.Value);
        if (student is null) return NotFound();
        await LoadGroupsAsync(student.GroupId);
        return View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Phone,GroupId")] Student student)
    {
        if (id != student.Id) return NotFound();
        if (!await context.Groups.AnyAsync(group => group.Id == student.GroupId))
            ModelState.AddModelError(nameof(student.GroupId), "Таңдалған топ табылмады.");

        if (ModelState.IsValid)
        {
            var existingStudent = await context.Students.FindAsync(id);
            if (existingStudent is null) return NotFound();

            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.Email = student.Email;
            existingStudent.Phone = student.Phone;
            existingStudent.GroupId = student.GroupId;
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        await LoadGroupsAsync(student.GroupId);
        return View(student);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var student = await context.Students.AsNoTracking()
            .Include(item => item.Group)
            .FirstOrDefaultAsync(item => item.Id == id.Value);
        return student is null ? NotFound() : View(student);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await context.Students.FindAsync(id);
        if (student is not null)
        {
            context.Students.Remove(student);
            await context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadGroupsAsync(int? selectedId = null)
    {
        ViewData["GroupId"] = new SelectList(
            await context.Groups.AsNoTracking().OrderBy(group => group.Name).ToListAsync(),
            nameof(StudentGroup.Id), nameof(StudentGroup.Name), selectedId);
    }
}
