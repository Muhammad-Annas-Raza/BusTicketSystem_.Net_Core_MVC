using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBusTicketReservationSystem.Interface;
using OnlineBusTicketReservationSystem.Models;

namespace OnlineBusTicketReservationSystem.Controllers
{
    [Authorize(Roles = "Conductor")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class ConductorController : Controller
    {
 
            private readonly IHttpContextAccessor context;

            private readonly IRepository<tbl_bus> Tbl_bus;
            private readonly IRepository<tbl_busSeats> Tbl_busSeats;
            private readonly IRepository<tbl_discount> Tbl_discount;
            private readonly IRepository<tbl_sale> Tbl_sale;
            private readonly IRepository<tbl_user> Tbl_user;
            

            public ConductorController
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


            public async Task<IActionResult> Index()
        {
            tbl_user? row = await Tbl_user.GetRowById(long.Parse(HttpContext.Session.GetString("UsrId") ?? "0"));
            tbl_bus row1 = await Tbl_bus.GetBusForConductor(row.bus_NumberPlateForConductor ?? "Nothing");
            List<tbl_busSeats> rows = await Tbl_busSeats.CollectAmount(row1.bus_id);
            ViewData["rows"] = rows;
            return View(row1);

        }
        [HttpGet]
        public async Task<IActionResult> EditBusDetails()
        {
            tbl_user? row = await Tbl_user.GetRowById(long.Parse(HttpContext.Session.GetString("UsrId") ?? "0"));
             
                tbl_bus row1 = await Tbl_bus.GetBusForConductor(row.bus_NumberPlateForConductor??"Nothing");            
                return View(row1);
             
        }
        [HttpPost]
        public async Task<IActionResult> EditBusDetails(tbl_bus b)
        {
            int a =await Tbl_bus.Update(b);
            if (a > 0)
            {
                TempData["msg11"] = "Bus Updated";
            }
            else 
            {
                TempData["msg12"] = "Failed to Updated";
            }
            return RedirectToAction("EditBusDetails","Conductor");
             
        }
        
        public async Task<IActionResult> AmountCollected(long id)
        {
            tbl_busSeats? row = await Tbl_busSeats.GetRowById(id);
            row.busSeat_customerReachedAmountCollected = true;
            int a = await Tbl_busSeats.Update(row);


            tbl_user? row1 = await Tbl_user.GetRowById(long.Parse(HttpContext.Session.GetString("UsrId") ?? "0"));
            tbl_bus row2 = await Tbl_bus.GetBusForConductor(row1.bus_NumberPlateForConductor ?? "Nothing");
            List<tbl_busSeats> rows = await Tbl_busSeats.CollectAmount(row2.bus_id);
            return PartialView("_Index",rows);
             
        }


        public async Task<IActionResult> CollectedAmount()
        {
            tbl_user? row = await Tbl_user.GetRowById(long.Parse(HttpContext.Session.GetString("UsrId") ?? "0"));
            tbl_bus row1 = await Tbl_bus.GetBusForConductor(row.bus_NumberPlateForConductor ?? "Nothing");
            List<tbl_busSeats> rows = await Tbl_busSeats.CollectedAmount(row1.bus_id);

            return View(rows);

        }
        //public async Task<IActionResult> AmountCollected(long id)
        //{
        //    tbl_user? row = await Tbl_user.GetRowById(long.Parse(HttpContext.Session.GetString("UsrId") ?? "0"));
        //    tbl_bus row1 = await Tbl_bus.GetBusForConductor(row.bus_NumberPlateForConductor ?? "Nothing");
        //    List<tbl_busSeats> rows = await Tbl_busSeats.CollectedAmount(row1.bus_id);

        //    return View(rows);

        //}
    }
}
