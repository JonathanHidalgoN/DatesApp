using System.ComponentModel.DataAnnotations;

namespace API;

public class RegisterDto{
    [Required]
    [MaxLength(40)]
    public required string userName { get; set; }
    [Required]
    [MaxLength(40)]
    public required string password { get; set; }
}