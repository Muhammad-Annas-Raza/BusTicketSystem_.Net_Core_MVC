using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineBusTicketReservationSystem.Controllers
{
    [Authorize(Roles = "Super Admin")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class SuperAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
