using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBusTicketReservationSystem.Interface;
using OnlineBusTicketReservationSystem.Models;

namespace OnlineBusTicketReservationSystem.Controllers
{
    [Authorize(Roles = "User")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class ServicesController : Controller
    {

        private readonly IHttpContextAccessor context;

        private readonly IRepository<tbl_bus> Tbl_bus;
        private readonly IRepository<tbl_busSeats> Tbl_busSeats;
        private readonly IRepository<tbl_discount> Tbl_discount;
        private readonly IRepository<tbl_sale> Tbl_sale;
        private readonly IRepository<tbl_user> Tbl_user;


        public ServicesController
            (
            IHttpContextAccessor context,

            IRepository<tbl_bus> Tbl_bus,
            IRepository<tbl_busSeats> Tbl_busSeats,
            IRepository<tbl_discount> Tbl_discount,
            IRepository<tbl_sale> Tbl_sale,
            IRepository<tbl_user> Tbl_user

            )
        {
            this.context = context;

            this.Tbl_bus = Tbl_bus;
            this.Tbl_busSeats = Tbl_busSeats;
            this.Tbl_discount = Tbl_discount;
            this.Tbl_sale = Tbl_sale;
            this.Tbl_user = Tbl_user;

        }


        public async Task<IActionResult> BookTicket(long id)
        {
            List<tbl_busSeats> seates = await Tbl_busSeats.GetBusSeats(id);
             
            return View();
        }
        public async Task<IActionResult> Express()
        {
            List<tbl_bus> rows = await Tbl_bus.GetExpressBus();
            if (rows.Count == 0)
            {
                TempData["msg13"] = "No Bus Available";
            }
            return View(rows);
        }
        public async Task<IActionResult> Luxury()
        {
            List<tbl_bus> rows = await Tbl_bus.GetLuxuryBus();
            if (rows.Count == 0)
            {
                TempData["msg13"] = "No Bus Available";
            }
            return View(rows);
        }
        public async Task<IActionResult> VolvoAC()
        {
            List<tbl_bus> rows = await Tbl_bus.GetVolvoACBus();
            if (rows.Count == 0)
            {
                TempData["msg13"] = "No Bus Available";
            }
            return View(rows);
        }
        public async Task<IActionResult> VolvoNonAC()
        {
            List<tbl_bus> rows = await Tbl_bus.GetVolvoNonACBus();
            if (rows.Count == 0)
            {
                TempData["msg13"] = "No Bus Available";
            }
            return View(rows);
        }
    }
}
