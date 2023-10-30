using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;

namespace Application.Dtos
{
    public class TransactionDto : BaseEntity
    {
        [RegularExpression("^\\d{1,8}(?:\\.\\d{1,2})?$",
            ErrorMessage = "The amount that you have sent is invalid \n example : 12345678.23")]
        [Range(0,Double.MaxValue, ErrorMessage = "the Amount should be more than Zero")]
        public double Amount { get; set; }
        [Required] public DateTime TransactionDate { get; set; }
        [Column(TypeName = "Varchar(250)")] public string? Description { get; set; }
        [ForeignKey("Category")][Required] public int CategoryId { get; set; }
        [ForeignKey("User")][Required] public int UserId { get; set; }
    }
}
