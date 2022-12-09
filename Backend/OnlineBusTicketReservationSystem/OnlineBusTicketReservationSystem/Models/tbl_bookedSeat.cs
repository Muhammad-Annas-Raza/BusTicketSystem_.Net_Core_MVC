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




        public int bookedSeat_customerTicketPrice{ get; set; }
        public int bookedSeat_customerDiscountPercentage{ get; set; }
        public int bookedSeat_customerDiscTicketPrice{ get; set; }  
        //public decimal bookedSeat_customerTicketPrice{ get; set; }
        //public decimal bookedSeat_customerDiscountPercentage{ get; set; }
        //public decimal bookedSeat_customerDiscTicketPrice{ get; set; }




        public bool bookedSeat_customerReached{ get; set; }
        [ForeignKey("tbl_bus")]
        [StringLength(50)]
        public string? fk_bus_id { get; set; }      //<One to Many Relation> One bus can have many booked seats
        public virtual tbl_bus? tbl_bus { get; set; }   //Navigational Property 

    }
}
