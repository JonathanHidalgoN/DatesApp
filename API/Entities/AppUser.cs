namespace API;

public class AppUser{
    public int id{get; set;}
    public required string userName {get; set;}

    public required byte[] passwordHash {get; set;}

    public required byte[] passwordSalt {get; set;}
}