using System.Security.Cryptography;
using System.Text;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

public class AccountController(DataContext context): BaseApiController{

    /**
    * This method is used to login a user
    * @param username: The username of the user
    * @param password: The password of the user
    * @return The user object
    */
    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> register(RegisterDto registerDto){
        if(await userExist(registerDto.userName)){
            return BadRequest("Username is taken");
        }

        using var hmac = new HMACSHA512();
        var user = new AppUser{
            userName = registerDto.userName.ToLower(),
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
            passwordSalt = hmac.Key
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    /**
    * This method is used to check if a user exists
    * @param username: The username of the user
    * @return A boolean value
    */
    private async Task<bool> userExist(string username){
        return await context.Users.AnyAsync(x => x.userName.ToLower() == username.ToLower());
    }
}