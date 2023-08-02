using System.ComponentModel.DataAnnotations;

namespace Application.Dtos;

public class UserCredentialDto
{
    [Required] public string UserName { get; set; }
    [Required] public string Password { get; set; }
}