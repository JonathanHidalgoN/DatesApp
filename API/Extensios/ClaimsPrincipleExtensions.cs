using System.Security.Claims;

namespace API;

public static class ClaimsPrincipleExtensions{

    public static string getUsername(this ClaimsPrincipal user){
        var username = user.FindFirstValue(ClaimTypes.Name);
        if(username == null) throw new Exception("User not found");
        return username;
    }

       public static int getUserId(this ClaimsPrincipal user){
        var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new Exception("User not found"));
        return userId;
    }
}
