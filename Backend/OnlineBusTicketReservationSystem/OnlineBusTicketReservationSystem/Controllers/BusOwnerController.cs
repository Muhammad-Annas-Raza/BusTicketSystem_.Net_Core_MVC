using Microsoft.AspNetCore.Authorization;
using OnlineBusTicketReservationSystem.Models;
using OnlineBusTicketReservationSystem.Services;
using Microsoft.AspNetCore.Mvc;
using OnlineBusTicketReservationSystem.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineBusTicketReservationSystem.Controllers
{
    [Authorize(Roles = "Bus Owner")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class BusOwnerController : Controller
    {

        private readonly IHttpContextAccessor context;
        private readonly IRepository<tbl_bookedSeat> Tbl_bookedSeat;
        private readonly IRepository<tbl_bus> Tbl_bus;
        private readonly IRepository<tbl_busSeats> Tbl_busSeats;
        private readonly IRepository<tbl_discount> Tbl_discount;
        private readonly IRepository<tbl_sale> Tbl_sale;
        private readonly IRepository<tbl_user> Tbl_user;

        public BusOwnerController
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
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Waiting()
        {
            return View();
        }
        public async Task<IActionResult> AddConductor()
        {
            List<tbl_bus> rows = await Tbl_bus.GetBusesfk_user_id(long.Parse(HttpContext.Session.GetString("UsrId") ?? "0"));
            ViewBag.busesNumberPlateList = rows.Select(x => new SelectListItem()
            {
                Text = x.bus_NumberPlate,
                Value = x.bus_NumberPlate
            }).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddConductor(tbl_user u)
        {
            if (u.user_name != null && u.user_email_phone != null && u.user_password != null && u.bus_NumberPlateForConductor != null)
            {
                tbl_user? row = await Tbl_user.GetRowById(long.Parse(HttpContext.Session.GetString("UsrId") ?? "0"));
                u.user_password = u.user_password.Encrypt_password();
                u.user_role = "Conductor";
                u.user_emailVerified = true;
                u.user_approved = true;
                u.organization_name = row.organization_name;
                u.Created_at = DateTime.Now;
                try
                {
                    int a = await Tbl_user.Create(u);
                    if (a > 0)
                    {
                        TempData["msg4"] = "Conductor Added";
                    }
                    else
                    {
                        TempData["msg5"] = "Failed to add Conductor";
                    }
                }
                catch (Exception)
                {

                    TempData["msg5"] = "Phone number is duplicate";
                }
                return RedirectToAction("AddConductor", "BusOwner");
            }
            else
            {
                TempData["msg5"] = "Please fill al fileds";
                return RedirectToAction("AddConductor", "BusOwner");
            }
        }
        public async Task<IActionResult> ViewConductor()
        {
            List<tbl_user> rows = await Tbl_user.GetAllConductors();
            return View(rows);
        }

        public async Task<IActionResult> DeleteConductor(long id)
        {
            await Tbl_user.Delete(id);
            List<tbl_user> rows = await Tbl_user.GetAllConductors();
            return PartialView("_ViewConductor", rows);
        }
        [HttpGet]
        public IActionResult AddBus()
        {            
            return View();
        }

      
        [HttpPost]
        public async Task<IActionResult> AddBus(tbl_bus b)
        {
            if (b.bus_NumberPlate != null && b.bus_ticketPrice != null && b.bus_noOfSeats != null && b.bus_category != null)
            {
                tbl_user? row = await Tbl_user.GetRowById(long.Parse(HttpContext.Session.GetString("UsrId") ?? "0"));
                
                
                b.bus_available = false;
                b.fk_user_id = long.Parse(HttpContext.Session.GetString("UsrId") ?? "0");
                b.Created_at = DateTime.Now;                 
                    int a = await Tbl_bus.Create(b);
                    if (a > 0)
                    {
                        TempData["msg6"] = "Bus Added";
                    for (int i = 1; i <= b.bus_noOfSeats; i++)
                    {
                        a = await Tbl_busSeats.Create(new tbl_busSeats() 
                        { 
                            busSeats_noOfSeats = i,
                            busSeats_isBooked = false,
                            Created_at = DateTime.Now,
                            fk_bus_id = b.bus_id,
                        }) ;
                        if (a<0)
                        {
                            break;
                        }
                    }
                    a = await Tbl_discount.Create(new tbl_discount()
                    {
                         discount_0_TO_5 = 0,
                         discount_6_TO_12 = 0,
                         discount_13_TO_50 = 0,
                         discount_51 = 0,
                         Created_at = DateTime.Now,
                         fk_bus_id = b.bus_id,
                    });

                }
                    else
                    {
                        TempData["msg7"] = "Failed to add Conductor";
                    }
                
                return RedirectToAction("AddBus", "BusOwner");
            }
            else
            {
                TempData["msg7"] = "Please fill al fileds";
                return RedirectToAction("AddBus", "BusOwner");
            }
        }


        public async Task<IActionResult> ViewBus()
        {
            List<tbl_bus> rows = await Tbl_bus.GetAllRows();
            return View(rows);
        }

        public async Task<IActionResult> DeleteBus(long id)
        {
            await Tbl_bus.Delete(id);
            List<tbl_bus> rows = await Tbl_bus.GetAllRows();
            return PartialView("_ViewBus", rows);
        }



       
        public async Task<IActionResult> ViewDiscount()
        {
            List<tbl_discount> rows = await Tbl_discount.GetDiscountInnerJoin();
            return View(rows);
        }
        
       
        public async Task<IActionResult> ClearDiscount(long id)
        {
            tbl_discount? row = await Tbl_discount.GetRowById(id);
            if (row != null)
            {
                row.discount_0_TO_5 = 0;
                row.discount_6_TO_12 = 0;
                row.discount_13_TO_50 = 0;
                row.discount_51 = 0;
                row.Created_at = DateTime.Now;
                await Tbl_discount.Update(row);
            }
            List<tbl_discount> rows = await Tbl_discount.GetDiscountInnerJoin();
            return PartialView("_ViewDiscount", rows);
        }
        
        
        public async Task<IActionResult> EditDiscount(long id)
        {
            tbl_discount? row = await Tbl_discount.GetRowById(id);
            return View(row);
        }
        [HttpPost]
        public async Task<IActionResult> EditDiscount(tbl_discount d)
        {
            d.Created_at = DateTime.Now;
            await Tbl_discount.Update(d);
            return RedirectToAction("ViewDiscount","BusOwner");
        }









}
}
