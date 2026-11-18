# StudentRegistrationSystem

ASP.NET Core MVC және Entity Framework Core негізіндегі студенттерді тіркеу жүйесі. Жоба 4-апта тапсырмасына арналған: CRUD операциялары, LINQ арқылы іздеу, топпен байланыс және форма тексерісі.

## Мүмкіндіктері

- Студенттерді қарау, қосу, өңдеу және өшіру.
- Аты немесе тегі бойынша `Where` арқылы іздеу.
- Тізімде студент тобын `Include` арқылы бірге жүктеу.
- EF Core асинхронды сұраныстары (`ToListAsync`, `SaveChangesAsync`, `FindAsync`).
- Data Annotations негізіндегі серверлік және клиенттік форма тексерісі.
- POST формаларында antiforgery токені.
- Алғашқы іске қосылғанда LocalDB дерекқоры мен үлгі деректерін жасау.

## Іске қосу

1. .NET 8 SDK және SQL Server LocalDB орнатыңыз.
2. `StudentRegistrationSystem.sln` шешімін Visual Studio-да ашыңыз немесе терминалда жоба бумасына өтіңіз.
3. `dotnet run --project StudentRegistrationSystem.csproj` командасын орындаңыз.
4. Браузерде көрсетілген localhost мекенжайын ашыңыз.

Дерекқордың LocalDB қосылу жолы `appsettings.json` ішінде берілген. Алғашқы іске қосылғанда үш топ және бірнеше үлгі студент жасалады.
