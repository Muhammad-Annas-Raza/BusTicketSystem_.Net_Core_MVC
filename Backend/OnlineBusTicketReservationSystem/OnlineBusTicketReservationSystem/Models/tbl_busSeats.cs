using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBusTicketReservationSystem.Models
{
    public class tbl_busSeats
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long busSeat_id{ get; set; }
        public int busSeat_SeatNumber{ get; set; }
        public bool busSeat_isBooked { get; set; }

        //================================================
        public int? busSeat_customerAge { get; set; }

        [Precision(18, 2)]
        public decimal? busSeat_customerTicketPrice { get; set; }
        [Precision(18, 2)]
        public decimal? busSeat_customerDiscountPercentage { get; set; }
        [Precision(18, 2)]
        public decimal? busSeat_customerDiscountedTicketPrice { get; set; }
        public string? busSeat_customerName { get; set; }
        public bool busSeat_customerReachedAmountCollected { get; set; }
        //================================================
        public DateTime Created_at { get; set; }
        [ForeignKey("tbl_bus")]
        public long fk_bus_id { get; set; }      //<One to Many Relation> One bus can have many seats
        public virtual tbl_bus? tbl_bus { get; set; }   //Navigational Property 


    }
}
