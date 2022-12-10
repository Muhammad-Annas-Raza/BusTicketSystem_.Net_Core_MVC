using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBusTicketReservationSystem.Models
{
    public class tbl_bookedSeat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long bookedSeat_id { get; set; }        
        public int bookedSeat_customerAge{ get; set; }

        //public int bookedSeat_customerTicketPrice{ get; set; }
        //public int bookedSeat_customerDiscountPercentage{ get; set; }
        //public int bookedSeat_customerDiscTicketPrice{ get; set; }
        [Precision(18, 2)]
        public decimal bookedSeat_customerTicketPrice { get; set; }
        [Precision(18,2)]
        public decimal bookedSeat_customerDiscountPercentage { get; set; }
        [Precision(18,2)]
        public decimal bookedSeat_customerDiscTicketPrice { get; set; }
        public string? Bookedseat_customerName { get; set; }
        public int? Bookedseat_customerSeatno { get; set; }

        public bool bookedSeat_customerReached{ get; set; }
        [ForeignKey("tbl_bus")]
        public long fk_bus_id { get; set; }      //<One to Many Relation> One bus can have many booked seats
        public virtual tbl_bus? tbl_bus { get; set; }   //Navigational Property 

    }
}
