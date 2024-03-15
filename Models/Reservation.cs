using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwimmingPool_V1.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("Lane")]
        public int? LaneId { get; set; }
        public Lane? Lane { get; set; }

        [ForeignKey("Pool")]
        public int? PoolId { get; set; }
        public Pool? Pool { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public DateTime DateTime { get; set; }
        public string? Type { get; set; }
        public int? Limit { get; set; }
        public string? Image { get; set; }
    }
}
