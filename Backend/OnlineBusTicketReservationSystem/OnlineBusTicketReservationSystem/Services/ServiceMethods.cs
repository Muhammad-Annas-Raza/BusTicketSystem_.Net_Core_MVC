using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies; 
using Microsoft.AspNetCore.Authorization; 
using OnlineBusTicketReservationSystem.Models;
using OnlineBusTicketReservationSystem.Interface;


namespace OnlineBusTicketReservationSystem.Services
{
    public class ServiceMethods 
    {
        private readonly IRepository<tbl_user> Tbl_user;
        public ServiceMethods(IRepository<tbl_user> Tbl_user)
        {
            this.Tbl_user = Tbl_user;
        }
        public async Task<int> AddSA()
        {
            tbl_user u = new tbl_user()
            {
                user_name = "Name of Super Admin",
                user_approved = true,
                user_emailVerified = true,
                user_password = "123".Encrypt_password(),
                user_email_phone = "annas@gmail.com",
                user_role = "Super Admin",
                user_verification_code = null, 
                Created_at = DateTime.Now
            };
            int a =await Tbl_user.Create(u);
            if (a > 0)
            {
                return 1;
            }
            else 
            {
                return -1;          
            }
        }

    }
}
