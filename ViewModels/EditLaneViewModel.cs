using SwimmingPool_V1.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SwimmingPool_V1.ViewModels
{
    public class EditLaneViewModel
    {
        public int LaneId { get; set; }
        public string? Type { get; set; }
        public int? Limit { get; set; }
        public string? Image { get; set; }
        public int PoolId { get; set; }
    }
}
