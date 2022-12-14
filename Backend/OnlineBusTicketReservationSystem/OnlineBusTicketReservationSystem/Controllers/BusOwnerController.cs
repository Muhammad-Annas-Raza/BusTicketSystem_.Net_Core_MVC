using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineBusTicketReservationSystem.Controllers
{
    [Authorize(Roles = "Bus Owner")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class BusOwnerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Waiting()
        {
            return View();
        }
    }
}
