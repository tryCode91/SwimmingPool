using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwimmingPool_V1.Models
{
    public class Lane
    {
        [Key]
        public int LaneId { get; set; }
        public string? Type { get; set; }
        public int? Limit { get; set; }
        public int? CurrentReserved { get; set; } = 0;
        [ForeignKey("Pool")]
        public int PoolId { get; set; }
        public Pool? Pool { get; set; }
        public string? Image { get; set; }
    }
}
