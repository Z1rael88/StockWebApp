using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Dtos.User;

public record RegisterDto
{
    [Required]
    public string? UserName { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required] 
    public string? Password { get; set; }
}