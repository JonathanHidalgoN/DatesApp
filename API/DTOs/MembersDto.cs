namespace API;

public class MembersDto{

    public int id{get; set;}
    public required string? userName {get; set;}

    public int age {get; set;}

    public string? photoUrl {get; set;}

    public required string? knownAs {get; set;}

    public DateTime created {get; set;}

    public DateTime lastActive {get; set;} 

    public required string? gender {get; set;}

    public string? introduction {get; set;}

     public string? interest {get; set;}

     public string? lookingFor {get; set;}

    public required string? city {get; set;}

    public required string? country {get; set;}

    public List<PhotoDto>? Photos {get; set;} = [];
}