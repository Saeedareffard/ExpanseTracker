using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.Entities
{
    [Table("UserCredentials")]
    public class UserCredentialModel: BaseEntity
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public string Password { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}