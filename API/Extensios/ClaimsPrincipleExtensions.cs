using System.Security.Claims;

namespace API;

public static class ClaimsPrincipleExtensions{

    public static string getUsername(this ClaimsPrincipal user){
        var username = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if(username == null) throw new Exception("User not found");
        return username;
    }
}