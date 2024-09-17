using System.Security.Cryptography;
using System.Text;
using API.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

public class AccountController(DataContext context, 
                            ITokenService tokenService, IMapper mapper): BaseApiController{

    /**
    * This method is used to login a user
    * @param username: The username of the user
    * @param password: The password of the user
    * @return The user object
    */
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> register(RegisterDto registerDto){
        if(await userExist(registerDto.username)){
            return BadRequest("username is taken");
        }
        using var hmac = new HMACSHA512();

        var user = mapper.Map<AppUser>(registerDto);
        user.username = registerDto.username.ToLower();
        user.passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password));
        user.passwordSalt = hmac.Key;
    
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return new UserDto{
            username = user.username,
            token = tokenService.createToken(user),
            knowAs = user.knownAs,
            gender = user.gender
        };
    }

    /**
    * This method is used to login a user
    * @param username: The username of the user
    * @param password: The password of the user
    * @return The user object
    */
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> login(LoginDto loginDto){
        var user = await context.Users.Include(p => p.Photos).
        FirstOrDefaultAsync(
            x => x.username == loginDto.username.ToLower()
        );
        if(user == null){
            return Unauthorized("Invalid username");
        }
        using var hmac = new HMACSHA512(user.passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));
        for(int i = 0; i < computedHash.Length; i++){
            if(computedHash[i] != user.passwordHash[i]){
                return Unauthorized("Invalid password");
            }
        }
        return new UserDto{
            username = user.username,
            token = tokenService.createToken(user),
            photoUrl = user.Photos.FirstOrDefault(x => x.isMain)?.url,
            knowAs = user.knownAs,
            gender = user.gender
        };
    }

    /**
    * This method is used to check if a user exists
    * @param username: The username of the user
    * @return A boolean value
    */
    private async Task<bool> userExist(string username){
        return await context.Users.AnyAsync(x => x.username.ToLower() == username.ToLower());
    }
}