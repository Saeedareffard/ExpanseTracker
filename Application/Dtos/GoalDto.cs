using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Application.Dtos;

public class GoalDto : BaseEntity
{
    [Required] public string Name { get; set; } = default!;

    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "The Goal Amount should be more than zero")]
    public double GoalAmount { get; set; }

    public DateTime? Deadline { get; set; }
    [Required] public double CurrentAmount { get; set; } = 0;

    public bool IsSuccessful = false;
}