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




        public int sale_totalAmountCollectedFromOneBus  { get; set; }
        //public decimal sale_totalAmountCollectedFromOneBus  { get; set; }





       
        [ForeignKey("tbl_bus")]
        [StringLength(50)]
        public string? fk_bus_id { get; set; }      //<One to One Relation> One bus can have One Profit(Sum of all ickets)
        public virtual tbl_bus? tbl_bus { get; set; }   //Navigational Property 

    }
}
