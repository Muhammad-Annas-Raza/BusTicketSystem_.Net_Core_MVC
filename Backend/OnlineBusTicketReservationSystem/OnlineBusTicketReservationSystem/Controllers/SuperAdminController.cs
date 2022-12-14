 using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBusTicketReservationSystem.Interface;
using OnlineBusTicketReservationSystem.Models;

namespace OnlineBusTicketReservationSystem.Controllers
{
    [Authorize(Roles = "Super Admin")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class SuperAdminController : Controller
    {
        private readonly IHttpContextAccessor context;
        private readonly IRepository<tbl_bookedSeat> Tbl_bookedSeat;
        private readonly IRepository<tbl_bus> Tbl_bus;
        private readonly IRepository<tbl_busSeats> Tbl_busSeats;
        private readonly IRepository<tbl_discount> Tbl_discount;
        private readonly IRepository<tbl_sale> Tbl_sale;
        private readonly IRepository<tbl_user> Tbl_user;

        public SuperAdminController
            (
            IHttpContextAccessor context,
            IRepository<tbl_bookedSeat> Tbl_bookedSeat,
            IRepository<tbl_bus> Tbl_bus,
            IRepository<tbl_busSeats> Tbl_busSeats,
            IRepository<tbl_discount> Tbl_discount,
            IRepository<tbl_sale> Tbl_sale,
            IRepository<tbl_user> Tbl_user
            )
        {
            this.context = context;
            this.Tbl_bookedSeat = Tbl_bookedSeat;
            this.Tbl_bus = Tbl_bus;
            this.Tbl_busSeats = Tbl_busSeats;
            this.Tbl_discount = Tbl_discount;
            this.Tbl_sale = Tbl_sale;
            this.Tbl_user = Tbl_user;
        }
        public async Task<IActionResult> Index()
        {

            List<tbl_user> rows = await Tbl_user.GetAllUnApprovedBusOwners();
            return View(rows);
        }

        public async Task<IActionResult> ViewBusOwners()
        {
            List<tbl_user> rows = await Tbl_user.GetAllApprovedBusOwners();
            return View(rows);

        }
        public async Task<IActionResult> ApproveBusOwner(long id)   //Using Ajax With .Net 6.0
        {
            await Tbl_user.ApproveBusOwner(id);
            //return RedirectToAction("Index", "SuperAdmin");
            List<tbl_user> rows = await Tbl_user.GetAllUnApprovedBusOwners();
            return PartialView("_Index", rows);
        }



        public async Task<IActionResult> DeleteBusOwner(long id)    //Using Ajax With .Net 6.0
        {
            await Tbl_user.Delete(id);
            List<tbl_user> rows = await Tbl_user.GetAllUnApprovedBusOwners();
            return PartialView("_Index", rows);
        }
        public async Task<IActionResult> DeleteBusOwner2(long id)
        {
            await Tbl_user.Delete(id);
            List<tbl_user> rows = await Tbl_user.GetAllApprovedBusOwners();
            return PartialView("_ViewBusOwners", rows);
        }

      
    }
}
