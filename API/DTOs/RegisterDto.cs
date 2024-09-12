using System.ComponentModel.DataAnnotations;

namespace API;

public class RegisterDto{
    [Required]
    [MaxLength(40)]
    public string username { get; set; } = string.Empty;
    [Required]
    [MaxLength(40)]
    public string password { get; set; } = string.Empty;

    [Required]
    public string? knowAs { get; set; }

    [Required]
    public string? gender { get; set; }

    [Required]
    public string? dateOfBirth { get; set; }

    [Required]
    public string? city { get; set; }

    [Required]
    public string? contry { get; set; }
}
