using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentDb")));

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<StudentDbContext>();
    await db.Database.EnsureCreatedAsync();
    if (!await db.Groups.AnyAsync())
    {
        db.Groups.AddRange(new StudentGroup { Name = "ИС-21" }, new StudentGroup { Name = "ИС-22" }, new StudentGroup { Name = "ПИ-21" });
        await db.SaveChangesAsync();
    }
    if (!await db.Students.AnyAsync())
    {
        var groups = await db.Groups.OrderBy(group => group.Id).ToListAsync();
        db.Students.AddRange(
            new Student { FirstName = "Айдана", LastName = "Серік", Email = "aidana@example.com", Phone = "+7 700 111 22 33", GroupId = groups[0].Id },
            new Student { FirstName = "Нұрлан", LastName = "Әлиев", Email = "nurlan@example.com", Phone = "+7 701 222 33 44", GroupId = groups[1].Id },
            new Student { FirstName = "Аружан", LastName = "Қасымова", Email = "aruzhan@example.com", GroupId = groups[2].Id });
        await db.SaveChangesAsync();
    }
}
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(name: "default", pattern: "{controller=Students}/{action=Index}/{id?}");
app.Run();
