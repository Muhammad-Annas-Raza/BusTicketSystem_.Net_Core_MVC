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
        public string? organization_name { get; set; } = null!; //For Bus Users
        public string? organization_description { get; set; }//For Bus Users
        [Column(TypeName = "text")]
        public string? organization_logo { get; set; }//For Bus Users
        [StringLength(150)]
        public string? bus_NumberPlateForConductor { get; set; }
        public long? user_id_ForConductor { get; set; }
        [NotMapped]
        public string? user_confirmPassword { get; set; } 

        [StringLength(50)]
        public string? user_role { get; set; }
        public DateTime Created_at { get; set; }

        public ICollection<tbl_bus> tbl_bus { get; set; }   //<Many to one relation> b/c many buses can belong to one user


    }
}
