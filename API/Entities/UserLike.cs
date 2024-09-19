namespace API;

public class UserLike{

    public AppUser sourceUser { get; set; } = null!;
    public int sourceUserId { get; set; }
    public AppUser targetUser { get; set; } = null!;
    public int targetUserId { get; set; }
}