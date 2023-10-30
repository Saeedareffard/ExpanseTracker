using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Application.Dtos
{
    public class GoalDto : BaseEntity
    {
        [Required] public string Name { get; set; } = default!;
        [Required]
        public double GoalAmount { get; set; }
        public DateTime? Deadline { get; set; }
        [Required]
        public double CurrentAmount { get; set; } = 0;

        public bool IsSuccessful = false;
    }
}
