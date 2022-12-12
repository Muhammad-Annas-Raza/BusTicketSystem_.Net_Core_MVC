using Microsoft.AspNetCore.Mvc;

namespace OnlineBusTicketReservationSystem.Controllers
{
    public class BusOwnerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Waiting()
        {
            return View();
        }
    }
}
