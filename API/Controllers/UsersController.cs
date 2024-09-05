 using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

[Authorize]
public class UsersController(IUserRepository userRepository) : BaseApiController{       
    /*
    Get all the users from the database
    */
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembersDto>>> getUsers(){
        var users = await userRepository.GetMembersAsync();
        return Ok(users);
    }

    /*
    Get just one user
    param id : Id of the user to fetch
    */
    [HttpGet("{username}")]
    public async Task<ActionResult<MembersDto>> getUser(string username){
        var user = await userRepository.GetMemberAsync(username);
        if(user == null){
            return NotFound();
        }
        return user;
    }

}