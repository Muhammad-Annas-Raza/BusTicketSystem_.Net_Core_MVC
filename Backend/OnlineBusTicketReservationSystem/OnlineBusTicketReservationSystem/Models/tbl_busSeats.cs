using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBusTicketReservationSystem.Models
{
    public class tbl_busSeats
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long busSeats_id{ get; set; }
        public int busSeats_noOfSeats{ get; set; }
        public bool busSeats_isBooked { get; set; }
        [ForeignKey("tbl_bus")]
        [StringLength(50)]
        public string? fk_bus_id { get; set; }      //<One to Many Relation> One bus can have many seats
        public virtual tbl_bus? tbl_bus { get; set; }   //Navigational Property 


    }
}
