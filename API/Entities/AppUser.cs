namespace API;

public class AppUser{
    public int id{get; set;}
    public required string userName {get; set;}

    public byte[] passwordHash {get; set;} = [];

    public byte[] passwordSalt {get; set;} = [];

    public DateOnly dateOfBirth {get; set;}

    public required string knownAs {get; set;}

    public DateTime created {get; set;} = DateTime.UtcNow;

    public DateTime lastActive {get; set;} = DateTime.UtcNow;

    public required string gender {get; set;}

    public string? introduction {get; set;}

     public string? interest {get; set;}

     public string? lookingFor {get; set;}

    public required string city {get; set;}

    public required string country {get; set;}

    public List<Photo> Photos {get; set;} = [];

}