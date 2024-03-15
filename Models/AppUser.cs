using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwimmingPool_V1.Models
{
    public class AppUser : IdentityUser
    {
        [Key]
        public string AppUserId { get; set; }
        public int? Age { get; set; }
        public ICollection<Lane> Lane { get; set; }
        public ICollection<Pool> Pool { get; set; }
        public ICollection<Reservation> Reservation { get; set; }

    }
}
