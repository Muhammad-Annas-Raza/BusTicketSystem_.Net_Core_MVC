using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBusTicketReservationSystem.Models
{
    public class tbl_history
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long history_id { get; set; }
        [StringLength(150)]
        public string? history_CustomerName { get; set; }
        [StringLength(10)]
        public string? history_CustomerAge { get; set; }
        [StringLength(10)]
        public string? history_CustomerTicketPrice { get; set; }
        [StringLength(10)]
        public string? history_CustomerDiscountPercentage { get; set; }
        [Precision(18, 2)]
        public decimal? history_CustomerDiscountedTicketPrice { get; set; }
        [StringLength(150)]
        public string? history_CustomerDestination { get; set; }    
        public int history_CustormerSeatno { get; set; }    
        [StringLength(150)]
        public string? history_CustomerBusOrganizationName { get; set; }        
        public DateTime history_CustomerBusStartingTime { get; set; }
        [StringLength(150)]
        public string? history_CustomerBusNumber { get; set; }
        [StringLength(150)]
        public string? history_CustomerBusCategory { get; set; }
        public DateTime Created_at { get; set; }

        [ForeignKey("tbl_user")]
        public long fk_user_id { get; set; }             //<One to many relation> b/c one User can have many history
        public virtual tbl_user? tbl_user { get; set; } //Navigational Property 


    }
}
