using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Dtos.User;

public record LoginDto
{
    [Required] public string UserName { get; set; }
    [Required] public string Password { get; set; }
}