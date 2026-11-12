using CampusPulse.Data;
using CampusPulse.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CampusDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CampusDb")));

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CampusDbContext>();
    db.Database.Migrate();
    if (!db.Events.Any())
    {
        db.Events.AddRange(
            new CampusEvent { Title = "Түнгі кино: Интерстеллар", Club = "Cinema Circle", Category = "Кино", Summary = "Ғарыш пен уақыт туралы фильмді бірге көріп, соңында пікірталас өткіземіз.", Venue = "A корпус · 204 аудитория", Organizer = "Аружан М.", StartsAt = DateTime.Now.AddDays(2).Date.AddHours(18), Capacity = 42, IsFeatured = true },
            new CampusEvent { Title = "Кампус фото серуені", Club = "Frame Lab", Category = "Шығармашылық", Summary = "Қаладағы қызықты бұрыштарды тауып, мобильді фотосурет түсіру тәсілдерін үйренеміз.", Venue = "Бас кітапхана алды", Organizer = "Данияр С.", StartsAt = DateTime.Now.AddDays(3).Date.AddHours(15), Capacity = 18 },
            new CampusEvent { Title = "Жасыл аула: сенбілік", Club = "Eco Step", Category = "Қоғамдық жұмыс", Summary = "Аулаға көшет отырғызып, кампусты таза ұстау бойынша шағын жоспар жасаймыз.", Venue = "Оңтүстік аула", Organizer = "Мадина К.", StartsAt = DateTime.Now.AddDays(5).Date.AddHours(10), Capacity = 30 },
            new CampusEvent { Title = "Board game evening", Club = "Tabletop Guild", Category = "Ойын", Summary = "Үстел ойындарын ойнап, жаңадан келгендермен танысу кеші.", Venue = "Студенттер үйі · Lounge", Organizer = "Ержан Т.", StartsAt = DateTime.Now.AddDays(6).Date.AddHours(17), Capacity = 24 }
        );
        db.SaveChanges();
    }
}
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(name: "default", pattern: "{controller=Events}/{action=Index}/{id?}");
app.Run();
