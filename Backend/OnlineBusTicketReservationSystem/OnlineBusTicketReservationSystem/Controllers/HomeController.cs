using Microsoft.AspNetCore.Mvc;
using OnlineBusTicketReservationSystem.Models;
using OnlineBusTicketReservationSystem.Interface;
using OnlineBusTicketReservationSystem.Services;
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
        private readonly ServiceMethods s;

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
            s = new ServiceMethods(Tbl_user);
        }
 
        public async Task<IActionResult> Index()
        {
            List<tbl_user> allrows = await Tbl_user.GetAllRows();
            if (allrows.Count == 0)
            {
                await s.AddSA();
                return View();
            }
            return View();
        }
 
        public IActionResult Login()
        {
            return View();
        }
 
        [HttpPost]
        public async Task<IActionResult> Login(tbl_user u)
        {
            if (u.user_email_phone != null && u.user_password != null)
            {
                tbl_user row =await Tbl_user.ChkCredentials(u.user_email_phone, u.user_password.Encrypt_password() ?? "");
                if (row == null)
                {
                    TempData["msg1"] = "User Not Found !!!";
                    return RedirectToAction("Login", "Home");
                }
                else if (row.user_emailVerified == false)
                {
                    HttpContext.Session.SetString("Name", row.user_name ?? "Nothing");
                    HttpContext.Session.SetString("Role", row.user_role ?? "Nothing");
                    HttpContext.Session.SetString("UsrId", row.user_id.ToString());
                    return RedirectToAction("emailVerification", "Home");
                }
                else
                {
                    HttpContext.Session.SetString("Name", row.user_name ?? "Nothing");
                    HttpContext.Session.SetString("Role", row.user_role ?? "Nothing");
                    HttpContext.Session.SetString("UsrId", row.user_id.ToString());
                    List<Claim> claims = new List<Claim>()
                {
                 new Claim(ClaimTypes.NameIdentifier,u.user_name?? "ABC Name")
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

                    if (HttpContext.Session.GetString("Role") == "User")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (HttpContext.Session.GetString("Role") ==  "Bus Owner")
                    {
                        return RedirectToAction("Index", "BusOwner");
                    }
                    else if (HttpContext.Session.GetString("Role") == "Super Admin")
                    {
                        return RedirectToAction("Index", "SuperAdmin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Conductor");
                    }
                }
           
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
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult BusOwnerRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BusOwnerRegister(tbl_user u1)
        {
            if (u1.user_password != null && u1.user_password == u1.user_confirmPassword)
            {
                //u1.user_password.Encrypt_password();
                if (u1.bus_NumberPlate != null)
                {
                    tbl_bus b = new tbl_bus() { bus_NumberPlate = u1.bus_NumberPlate };
                    string code = DateTime.Now.ToString("fffffff");
                    u1.user_password = u1.user_password.Encrypt_password();
                    u1.user_verification_code = code;
                    u1.user_emailVerified = false;
                    u1.user_approved = false;
                    u1.user_role = "Bus Owner";
                   
                        if (u1.user_email_phone != null)
                        {
                        List<tbl_user> AllRows = await Tbl_user.GetAllRows();
                        foreach (tbl_user i in AllRows)
                        {
                            if (i.user_email_phone == u1.user_email_phone)
                            {
                                TempData["msg2"] = "Email is Duplicate";
                                return RedirectToAction("BusOwnerRegister", "Home");
                            }
                        }
                        _ = Tbl_user.Create(u1);                             
                            u1.user_email_phone.Send_email(code);
                        _ = Tbl_bus.Create(b);
                            return RedirectToAction("Login","Home");

                        }
                  
                }
                else
                {
                    TempData["msg2"] = "Bus Number cannot be empty";
                    return RedirectToAction("BusOwnerRegister", "Home");
                }
            }
            else
            {
                TempData["msg2"] = "Password doesnot match";
                return RedirectToAction("BusOwnerRegister", "Home");
            }
            return View();
        }        
        public IActionResult UserRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserRegister(tbl_user u)
        {
            if (u.user_password != null && u.user_password == u.user_confirmPassword)
            {         
               string code = DateTime.Now.ToString("fffffff");
                u.user_password = u.user_password.Encrypt_password();
                u.user_emailVerified = false;
                u.user_verification_code = code;
                u.user_approved = true;
                u.user_role = "User";
               
                    if (u.user_email_phone != null)
                {
                    List<tbl_user> AllRows = await Tbl_user.GetAllRows();
                    foreach (tbl_user i in AllRows)
                    {
                        if (i.user_email_phone == u.user_email_phone)
                        {
                            TempData["msg2"] = "Email is Duplicate";
                            return RedirectToAction("UserRegister", "Home");
                        }
                    }
                        _ = Tbl_user.Create(u);                   
                        u.user_email_phone.Send_email(code);
                        return RedirectToAction("Login", "Home");
                    }
                             
            }
            else
            {
                TempData["msg2"] = "Password doesnot match";
                return RedirectToAction("UserRegister", "Home");
            }
            return View();
        }
        public IActionResult emailVerification()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> emailVerification(string Verification_code)
        {
            long id = long.Parse(HttpContext.Session.GetString("UsrId"));
            string res =await Tbl_user.VerifyCode(id,Verification_code);
            if (res == "true")
            {
                if (HttpContext.Session.GetString("Role") == "User")
                {
                    return RedirectToAction("Login", "Home");
                }
                else if (HttpContext.Session.GetString("Role") == "Bus Owner")
                {
                    return RedirectToAction("Index","BusOwner");
                }
                    return RedirectToAction("Login", "Home");
            
            }else if(res == "Verified but not approved by user")
            {
                return RedirectToAction("Waiting", "BusOwner");
            }
            else
            {
                TempData["msg3"] = "Verification Code is incorrect";
                return RedirectToAction("emailVerification", "Home");
            }
           
        }

   

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
  
    }
}