using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Application.Dtos
{
    public class CategoryDto : BaseEntity
    {
        [Required] public string Name { get; set; }
    }
}
