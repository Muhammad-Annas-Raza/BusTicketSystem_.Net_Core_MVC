using Microsoft.AspNetCore.Mvc;
using OnlineBusTicketReservationSystem.Models;
using OnlineBusTicketReservationSystem.Interface;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace OnlineBusTicketReservationSystem.Controllers
{

    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor context;
        private readonly ILogger<HomeController> logger;
        private readonly IRepository<tbl_bookedSeat> Tbl_bookedSeat;
        private readonly IRepository<tbl_bus> Tbl_bus ;
        private readonly IRepository<tbl_busSeats> Tbl_busSeats;
        private readonly IRepository<tbl_discount> Tbl_discount;
        private readonly IRepository<tbl_sale> Tbl_sale;
        private readonly IRepository<tbl_user> Tbl_user;

        public HomeController
            (
            IHttpContextAccessor context,
            ILogger<HomeController> logger ,
            IRepository<tbl_bookedSeat> Tbl_bookedSeat,
            IRepository<tbl_bus> Tbl_bus,
            IRepository<tbl_busSeats> Tbl_busSeats,
            IRepository<tbl_discount> Tbl_discount,
            IRepository<tbl_sale> Tbl_sale,
            IRepository<tbl_user> Tbl_user
            )
        {
            this.context = context;
            this.logger = logger;
            this.Tbl_bookedSeat = Tbl_bookedSeat ;
            this.Tbl_bus  = Tbl_bus  ;
            this.Tbl_busSeats= Tbl_busSeats;
            this.Tbl_discount = Tbl_discount ;
            this.Tbl_sale = Tbl_sale ;
            this.Tbl_user = Tbl_user ;
        }
 
        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult Login()
        {
            return View();
        }
 
        [HttpPost]
        public async Task<IActionResult> Login(tbl_user u)
        {
            if (u.user_password == "123")
            {
                HttpContext.Session.SetString("Name", u.user_name);
                HttpContext.Session.SetString("Role", "ABC");                
                List<Claim> claims = new List<Claim>() 
                { 
                 new Claim(ClaimTypes.NameIdentifier,u.user_name)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = false
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index");
            }
            else
            {
                TempData["msg1"] = "User Not Found !!!";
                return RedirectToAction("Login", "Home");
            }
        }
 
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("Role");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }

        [Authorize]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Feedback()
        {
            return View();
        }

        [Authorize]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public IActionResult Feedback(tbl_feedback f)
        {
            return View();
        }

        public IActionResult BusOwnerRegister()
        {
            return View();
        }

        public IActionResult UserRegister()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}