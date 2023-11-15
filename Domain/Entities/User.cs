using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.Entities;

[Table("User")]
public class User : BaseEntity
{
    [Required] public string UserName { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Email { get; set; }
    [JsonIgnore] public virtual ICollection<Transaction>? Transactions { get; set; } = default;
    [JsonIgnore]
    public virtual UserCredentialModel? Credential { get; set; }
}