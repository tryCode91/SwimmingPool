using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace SwimmingPool_V1.Models
{
    public class Pool
    {
        [Key]
        public int PoolId { get; set; }
        public string? PoolName { get; set; }
        public string? Location { get; set; }
        public TimeOnly Open { get; set; }
        public TimeOnly Close { get; set; }
        public ICollection<Lane> Lane { get; set; } // get collection for the properties in lane model
    }
}
