using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        [Required] public string Name { get; set; } = null;
        [JsonIgnore]
        public virtual List<Transaction>? Transactions { get; set; }

    }
}
