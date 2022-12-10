using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBusTicketReservationSystem.Models
{
    public class tbl_feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long feedback_id{ get; set; }
        public string? feedback_username{ get; set; }
        public string? feedback_useremail{ get; set; }
        public string? feedback_subject{ get; set; }
        [Column(TypeName = "text")]
        public string? feedback_Message{ get; set; }
    }
}
