using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.Entities;

[Table("ExpanseTransaction")]
public class Transaction : BaseEntity
{
    [RegularExpression("^\\d{1,8}(?:\\.\\d{1,2})?$",
        ErrorMessage = "The amount that you have sent is invalid \n example : 12345678.23")]
    [Column(TypeName = "decimal(10,2)")]
    [Required]
    public double Amount { get; set; }

    [Required] public DateTime TransactionDate { get; set; }

    [Column(TypeName = "Varchar(250)")] public string? Description { get; set; }
   public virtual Category? Category { get; set; }
    [ForeignKey("Category")] [Required] public int CategoryId { get; set; }
    [ForeignKey("User")] [Required] public int UserId { get; set; }
}