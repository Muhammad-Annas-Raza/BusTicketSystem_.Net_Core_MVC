using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBusTicketReservationSystem.Models
{
    [Index(nameof(bus_NumberPlate), IsUnique = true)]
    public class tbl_bus
    {

        public tbl_bus()
        {
            tbl_busSeats = new HashSet<tbl_busSeats>();
            //tbl_bookedSeat = new HashSet<tbl_bookedSeat>();
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long bus_id { get; set; }
        [StringLength(50)]
        public string bus_NumberPlate { get; set; } = null!;  //Number Plate of Bus
          
        public DateTime? bus_startingTime{ get; set; }        
        public string? bus_destination { get; set; }
        //public int? bus_ticketPrice{ get; set; }
        [Precision(18, 2)]
        public decimal? bus_ticketPrice{ get; set; }
        public int? bus_noOfSeats { get; set; }
        [StringLength(50)]
        [Required]
        public string? bus_category{ get; set; }
        [StringLength(50)]
        public string? bus_OrganizationName { get; set; }
        public bool bus_available{ get; set; }
        public DateTime Created_at { get; set; }
        [ForeignKey("tbl_user")]
        public long fk_user_id{ get; set; }             //<One to many relation> b/c one User can have many buses
        public virtual tbl_user? tbl_user { get; set; } //Navigational Property 


        //<summary>     
        // Navigational Property of collection seats of one bus
        // <One to Many Relation> One bus can have many seats
        // </summary>  
        public virtual ICollection<tbl_busSeats>? tbl_busSeats { get; set; }

        //<summary>     
        // Navigational Property of collection of booked seats of one bus
        // <One to Many Relation> One bus can have many booked seats
        // </summary>  
        //public virtual ICollection<tbl_bookedSeat>? tbl_bookedSeat { get; set; }

        
        //<summary>     
        // Navigational Property of Sale Of one bus
        // <One to One Relation> One bus can have One Dicount
        // </summary>  
        public virtual tbl_discount tbl_discount { get; set; }
    

    }
}
