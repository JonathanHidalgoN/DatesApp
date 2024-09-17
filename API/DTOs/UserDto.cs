namespace API;

public class UserDto{
    public required string username { get; set; }
    public required string token { get; set; }

    public string? photoUrl { get; set; }

    public required string? knowAs { get; set; }

    public required string gender;
}