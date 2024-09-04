using System.ComponentModel.DataAnnotations;

namespace API;

public class RegisterDto{
    [Required]
    [MaxLength(40)]
    public string userName { get; set; } = string.Empty;
    [Required]
    [MaxLength(40)]
    public string password { get; set; } = string.Empty;
}