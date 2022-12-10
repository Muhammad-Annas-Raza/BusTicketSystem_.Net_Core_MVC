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
        public int sale_noOfSeatsSale { get; set; }

            public DateTime Sale_busStartingTime { get; set; }
            public string? Sale_busDestination { get; set; }
            public string? Sale_busOrganizationName { get; set; }
            public string? Sale_busCategory { get; set; }

        //public int sale_totalAmountCollectedFromOneBus  { get; set; }
        [Precision(18, 2)]
        public decimal sale_totalAmountCollectedFromOneBus  { get; set; }

        [ForeignKey("tbl_bus")]
        public long fk_bus_id { get; set; }      //<One to One Relation> One bus can have One Profit(Sum of all ickets)
        public virtual tbl_bus? tbl_bus { get; set; }   //Navigational Property 

    }
}
