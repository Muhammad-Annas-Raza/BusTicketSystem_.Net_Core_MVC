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
        private readonly IRepository<tbl_history> Tbl_history;


        public ServicesController
            (
            IHttpContextAccessor context,

            IRepository<tbl_bus> Tbl_bus,
            IRepository<tbl_busSeats> Tbl_busSeats,
            IRepository<tbl_discount> Tbl_discount,
            IRepository<tbl_sale> Tbl_sale,
            IRepository<tbl_user> Tbl_user,
            IRepository<tbl_history> Tbl_history

            )
        {
            this.context = context;

            this.Tbl_bus = Tbl_bus;
            this.Tbl_busSeats = Tbl_busSeats;
            this.Tbl_discount = Tbl_discount;
            this.Tbl_sale = Tbl_sale;
            this.Tbl_user = Tbl_user;
            this.Tbl_history = Tbl_history;

        }


        public async Task<IActionResult> BookTicket(long id)
        {
            tbl_bus? bus = await Tbl_bus.GetRowById(id);
            ViewBag.TicketPrice = bus.bus_ticketPrice;
            List<tbl_busSeats> seates = await Tbl_busSeats.GetBusSeats(id);
             
            return View(seates);
        }
        long c;

        [HttpPost]
        public async Task<IActionResult> BookTicket(long bus_id,decimal ticket_price, long busSeat_id, string name, int age)
        {

            if (bus_id != 0 && busSeat_id != 0 && name != null && age != 0)
            {
                decimal amount;
                tbl_bus? bus = await Tbl_bus.GetRowById(bus_id);
                tbl_discount? discount = await Tbl_discount.GetRowById(bus_id);
                tbl_busSeats? seat = await Tbl_busSeats.GetRowById(busSeat_id);
                if (bus != null && discount != null && seat != null && discount.discount_0_TO_5 != null && discount.discount_6_TO_12 != null
                     && discount.discount_13_TO_50 != null
                      && discount.discount_51 != null
                      )
                {
                    
                    seat.busSeat_customerTicketPrice = ticket_price;
                    seat.busSeat_isBooked = true;
                    seat.busSeat_customerName = name;
                    seat.busSeat_customerAge = age;                     
                    if (age > 0 && age < 6)
                    {
                        seat.busSeat_customerDiscountPercentage = discount.discount_0_TO_5;
                        amount = (ticket_price/100)*(decimal)discount.discount_0_TO_5;
                        seat.busSeat_customerDiscountedTicketPrice = ticket_price - amount;



                    }else if (age > 6 && age < 13)
                    {
                        seat.busSeat_customerDiscountPercentage = discount.discount_6_TO_12;
                          amount = (ticket_price/100)*(decimal)discount.discount_6_TO_12;
                        seat.busSeat_customerDiscountedTicketPrice = ticket_price - amount;

                    }else if (age > 13 && age < 51)
                    {
                        seat.busSeat_customerDiscountPercentage = discount.discount_13_TO_50;
                          amount = (ticket_price/100)*(decimal)discount.discount_13_TO_50;
                        seat.busSeat_customerDiscountedTicketPrice = ticket_price - amount;

                    }else if (age > 51)
                    {
                        seat.busSeat_customerDiscountPercentage = discount.discount_51;
                        amount = (ticket_price / 100) * (decimal)discount.discount_51;
                        seat.busSeat_customerDiscountedTicketPrice = ticket_price - amount;

                    }
 
                await Tbl_busSeats.Update(seat);
                    tbl_busSeats? Updatedseat = await Tbl_busSeats.GetRowById(busSeat_id);
                    tbl_history obj = new tbl_history()
                    {
                        history_CustomerName = name,
                        Created_at = DateTime.Now,
                        fk_user_id = long.Parse(HttpContext.Session.GetString("UsrId") ?? "0"),
                        history_CustomerAge = age.ToString(),
                        history_CustomerTicketPrice = ticket_price.ToString(),
                        history_CustomerDiscountPercentage = Updatedseat.busSeat_customerDiscountPercentage.ToString(),
                        history_CustomerDiscountedTicketPrice = Updatedseat.busSeat_customerDiscountedTicketPrice,
                        history_CustomerDestination = bus.bus_destination,
                        history_CustomerBusCategory = bus.bus_category,
                        history_CustomerBusNumber = bus.bus_NumberPlate,
                        history_CustomerBusStartingTime = (DateTime)bus.bus_startingTime,
                        history_CustomerBusOrganizationName = bus.bus_OrganizationName,
                        history_CustormerSeatno = Updatedseat.busSeat_SeatNumber

                    };
                    await Tbl_history.Create(obj);
                     c = obj.history_id;
                }
                return RedirectToAction("Ticket", "Services", new { id = c });

            }
            else if(bus_id == 0 && busSeat_id ==0 && age == 0 && name == null)
            {
     
              TempData["msg17"] = "Please fill all fields"  ;
                return RedirectToAction("BookTicket","Services");
            }
            else if(busSeat_id ==0 )
            {
     
              TempData["msg17"] = "Please select seat";
                return RedirectToAction("BookTicket","Services");
            }
            else if(name == null )
            {
     
              TempData["msg17"] = "Please fill passenger name";
                return RedirectToAction("BookTicket","Services");
            }
            else 
            {
     
              TempData["msg17"] = "Please fill passenger age";
                return RedirectToAction("BookTicket","Services");
            }

          

        }
        public async Task<IActionResult> Ticket(long id)
        {
            tbl_history? h =await Tbl_history.GetRowById(id);
            return View(h);
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
        public async Task<IActionResult> History()
        {
            List<tbl_history> rows = await Tbl_history.GetUserHistory(long.Parse(HttpContext.Session.GetString("UsrId") ?? "0"));
            if (rows.Count == 0)
            {
                TempData["msg19"] = "No Tickets Booked";
            }
            return View(rows);
        }
    }
}
