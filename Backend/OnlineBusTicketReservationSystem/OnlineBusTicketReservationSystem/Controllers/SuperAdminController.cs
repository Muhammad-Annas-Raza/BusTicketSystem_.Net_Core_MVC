 using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBusTicketReservationSystem.Interface;
using OnlineBusTicketReservationSystem.Services;
using OnlineBusTicketReservationSystem.Models;

namespace OnlineBusTicketReservationSystem.Controllers
{
 
    public class SuperAdminController : Controller
    {
        private readonly IHttpContextAccessor context;

        private readonly IRepository<tbl_bus> Tbl_bus;
        private readonly IRepository<tbl_busSeats> Tbl_busSeats;
        private readonly IRepository<tbl_discount> Tbl_discount;
        private readonly IRepository<tbl_sale> Tbl_sale;
        private readonly IRepository<tbl_user> Tbl_user;
        private readonly IWebHostEnvironment hostEnvironment;

        public SuperAdminController
            (
            IHttpContextAccessor context,

            IRepository<tbl_bus> Tbl_bus,
            IRepository<tbl_busSeats> Tbl_busSeats,
            IRepository<tbl_discount> Tbl_discount,
            IRepository<tbl_sale> Tbl_sale,
            IRepository<tbl_user> Tbl_user,
            IWebHostEnvironment hostEnvironment
            )
        {
            this.context = context;
            this.Tbl_bus = Tbl_bus;
            this.Tbl_busSeats = Tbl_busSeats;
            this.Tbl_discount = Tbl_discount;
            this.Tbl_sale = Tbl_sale;
            this.Tbl_user = Tbl_user;
            this.hostEnvironment = hostEnvironment;
        }
        [Authorize(Roles = "Super Admin")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index()
        {

            List<tbl_user> rows = await Tbl_user.GetAllUnApprovedBusOwners();
            return View(rows);
        }


        [Authorize(Roles = "Super Admin,User,Conductor,Bus Owner")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Profile()
        {
            tbl_user? row = await Tbl_user.GetRowById(long.Parse(HttpContext.Session.GetString("UsrId") ?? "0"));
            return View(row);
        }

           [Authorize(Roles = "Super Admin")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ViewBusOwners()
        {
            List<tbl_user> rows = await Tbl_user.GetAllApprovedBusOwners();
            return View(rows);

        }
           [Authorize(Roles = "Super Admin")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ApproveBusOwner(long id)   //Using Ajax With .Net 6.0
        {
            await Tbl_user.ApproveBusOwner(id);
            //return RedirectToAction("Index", "SuperAdmin");
            List<tbl_user> rows = await Tbl_user.GetAllUnApprovedBusOwners();
            return PartialView("_Index", rows);
        }



           [Authorize(Roles = "Super Admin")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> DeleteBusOwner(long id)    //Using Ajax With .Net 6.0
        {
            tbl_user? row = await Tbl_user.GetRowById(id);
            
            await Tbl_user.Delete(id);
            string wwwroot = hostEnvironment.WebRootPath;
            if (row != null)
            {
                if (System.IO.File.Exists(Path.Combine(wwwroot + "/Organization_Logo", row.organization_logo ?? "Nothing")))
                {
                    System.IO.File.Delete(Path.Combine(wwwroot + "/Organization_Logo", row.organization_logo ?? "Nothing"));
                }
            }
            List<tbl_user> rows = await Tbl_user.GetAllUnApprovedBusOwners();
            return PartialView("_Index", rows);
        }
           [Authorize(Roles = "Super Admin")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> DeleteBusOwner2(long id)
        {
            tbl_user? row = await Tbl_user.GetRowById(id);
            List<tbl_user> conductors =await Tbl_user.GetAllSpecificConductors(id);
            foreach (tbl_user i in conductors)
            {
                await Tbl_user.Delete(i.user_id);
            }
            await Tbl_user.Delete(id);
            string wwwroot = hostEnvironment.WebRootPath;
            if (row != null)
            {
                if (System.IO.File.Exists(Path.Combine(wwwroot + "/Organization_Logo", row.organization_logo ?? "Nothing")))
                {
                    System.IO.File.Delete(Path.Combine(wwwroot + "/Organization_Logo", row.organization_logo ?? "Nothing"));
                }
            }
            List<tbl_user> rows = await Tbl_user.GetAllApprovedBusOwners();
            return PartialView("_ViewBusOwners", rows);
        }
        [Authorize(Roles = "Super Admin,User,Conductor,Bus Owner")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ChangePassword1(string pwd)
        {
            tbl_user? row = await Tbl_user.GetRowById(long.Parse(HttpContext.Session.GetString("UsrId") ?? "0"));
            if (pwd.Encrypt_password() == row.user_password)
            {
                return PartialView("_chgPwd");
            }
            else
            {
                return Json("<h4 class=\"text-danger\">Password is incorrect</h4>");
            }
        } 
        [Authorize(Roles = "Super Admin,User,Conductor,Bus Owner")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ChangePassword2(tbl_user u)
        {
            tbl_user? row = await Tbl_user.GetRowById(long.Parse(HttpContext.Session.GetString("UsrId") ?? "0"));
            if (u.user_password == null) 
            {
                return Json("<h4 class=\"text-danger\">Password can not be empty</h4>");
            } else if (u.user_password != null && u.user_password == u.user_confirmPassword && row != null)
            {
                row.user_password = u.user_password.Encrypt_password();
                await Tbl_user.Update(row);
                return Json("<h4 class=\"text-success\">Password Changed</h4>");
            }
            else
            {
                ModelState.Clear();
                return Json("<h4 class=\"text-danger\">Password does not match</h4>");
            }
        }
        [Authorize(Roles = "Super Admin,User,Conductor,Bus Owner")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public async Task<IActionResult> ChangeProfileName(string name)
        {
            tbl_user? row = await Tbl_user.GetRowById(long.Parse(HttpContext.Session.GetString("UsrId") ?? "0"));
            if (name != null && row != null)
            {
                row.user_name = name;
                int a = await Tbl_user.Update(row);
                if (a>0)
                {
                    HttpContext.Session.SetString("Name", name);
                    return RedirectToAction("Profile","SuperAdmin");
                }
                 
            }
            return RedirectToAction("Profile", "SuperAdmin");
        }

    }
}
