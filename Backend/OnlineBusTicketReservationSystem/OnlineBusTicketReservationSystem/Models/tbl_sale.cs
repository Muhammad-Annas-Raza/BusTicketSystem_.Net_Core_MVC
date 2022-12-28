using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBusTicketReservationSystem.Models
{
    public class tbl_sale
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long sale_id { get; set; }
 
        public int sale_TotalnoOfSeats { get; set; }
        public int sale_noOfSoldSeats { get; set; }
            public DateTime Sale_busStartingTime { get; set; }
            public string? Sale_busDestination { get; set; }
            public string? Sale_busOrganizationName { get; set; }
            public string? Sale_busCategory { get; set; }

        //public int sale_totalAmountCollectedFromOneBus  { get; set; }
        [Precision(18, 2)]
        public decimal sale_totalAmountCollectedFromOneBus  { get; set; }
        public DateTime Created_at { get; set; }
        [ForeignKey("tbl_user")]
        public long fk_user_id { get; set; }             //<One to many relation> b/c one User can have many buses
        public virtual tbl_user? tbl_user { get; set; } //Navigational Property 


    }
}
