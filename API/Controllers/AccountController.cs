using System.Security.Cryptography;
using System.Text;
using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class AccountController(DataContext context): BaseApiController{

    /**
    * This method is used to login a user
    * @param username: The username of the user
    * @param password: The password of the user
    * @return The user object
    */
    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> register(string username, string password){
        using var hmac = new HMACSHA512();
        var user = new AppUser{
            userName = username,
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            passwordSalt = hmac.Key
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }
}