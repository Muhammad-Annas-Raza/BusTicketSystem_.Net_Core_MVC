using Microsoft.EntityFrameworkCore;

namespace OnlineBusTicketReservationSystem.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            :base(options)
        {
                
        }

        public DbSet<tbl_user> tbl_user { get; set; }
        public DbSet<tbl_sale> tbl_sale { get; set; }
        public DbSet<tbl_discount> tbl_discount{ get; set; }
        public DbSet<tbl_busSeats> tbl_busSeats { get; set; }
        public DbSet<tbl_bus> tbl_bus { get; set; }
        public DbSet<tbl_bookedSeat> tbl_bookedSeat { get; set; }
}
}
