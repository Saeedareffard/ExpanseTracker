using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Goal : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double GoalAmount { get; set; }
        public DateTime? Deadline { get; set; }
        [Required]
        public double CurrentAmount { get; set; } = 0;

        [NotMapped]
        public bool IsSuccessful => GoalAmount == CurrentAmount;
    }
}