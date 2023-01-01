using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBusTicketReservationSystem.Models
{
    [Index(nameof(fk_bus_id), IsUnique = true)]
    public class tbl_discount
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long discount_id{ get; set; }

        //public int? discount_0_TO_5{ get; set; }
        //public int? discount_6_TO_12{ get; set; }
        //public int? discount_13_TO_50{ get; set; }
        //public int? discount_51{ get; set; }
        [Precision(18, 2)]
        public decimal? discount_0_TO_5 { get; set; }
        [Precision(18, 2)]
        public decimal? discount_6_TO_12 { get; set; }
        [Precision(18,2)]
        public decimal? discount_13_TO_50 { get; set; }
        [Precision(18,2)]
        public decimal? discount_51 { get; set; }
        public DateTime Created_at { get; set; }
        [NotMapped]
        public string? bus_number { get; set; }
        [ForeignKey("tbl_bus")]
        public long fk_bus_id{ get; set; }
        public tbl_bus tbl_bus { get; set; }   //Navigational Property  

    }
}
