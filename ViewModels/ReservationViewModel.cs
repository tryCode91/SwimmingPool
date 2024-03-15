using SwimmingPool_V1.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SwimmingPool_V1.ViewModels
{
    public class ReservationViewModel
    {
        public int ReservationId { get; set; }
        public string GUID { get; set; } = Guid.NewGuid().ToString();
        public int? LaneId { get; set; }
        public Lane? Lane { get; set; }
        public int? PoolId { get; set; }
        public Pool? Pool { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public DateTime DateTime { get; set; }
        public string? Type { get; set; }
        public int? Limit { get; set; }
        public string? Image { get; set; }
    }
}
