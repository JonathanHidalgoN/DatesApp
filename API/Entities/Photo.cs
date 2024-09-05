 using System.ComponentModel.DataAnnotations.Schema;

namespace API;
[Table("Photos")] 
public class Photo{

    public int id {get; set;}

    public required string url {get; set;}

    public bool isMain {get; set;}

    public string? publicId {get; set;}

    public int AppUserId {get; set;}

    public AppUser AppUser {get; set;} = null!;
}