using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBusTicketReservationSystem.Models
{
    [Index(nameof(user_email_phone), IsUnique = true)]
    public class tbl_user
    {
        public tbl_user()
        {
            tbl_bus = new HashSet<tbl_bus>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long user_id { get; set; }
        [StringLength(150)]
        public string? user_name{ get; set; }
        [StringLength(150)]
        public string? user_password { get; set; }
        [StringLength(150)]
        [Required]
        public string? user_email_phone { get; set; }
        [StringLength(150)]
        public string? user_verification_code { get; set; }
        public bool user_emailVerified { get; set; }
        public bool user_approved { get; set; }

        [StringLength(50)]
        public string? user_role { get; set; }              //<One to One relation> b/c one bus can have one time discounts

        public ICollection<tbl_bus> tbl_bus { get; set; }   //<Many to one relation> b/c many buses can belong to one user


    }
}
