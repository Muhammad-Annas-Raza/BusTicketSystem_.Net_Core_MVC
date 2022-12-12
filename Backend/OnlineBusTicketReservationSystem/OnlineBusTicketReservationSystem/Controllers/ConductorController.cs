using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineBusTicketReservationSystem.Controllers
{
    [Authorize(Roles = "Conductor")]
    public class ConductorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
