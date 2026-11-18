using Microsoft.AspNetCore.Mvc;

namespace StudentRegistrationSystem.Controllers;

public class HomeController : Controller
{
    [Route("Home/Error")]
    public IActionResult Error() => View("~/Views/Shared/Error.cshtml");
}
