using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController{       
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

    /*
    Update the user
    param memberUpdateDto : The data to update
    */
    [HttpPut]
    public async Task<ActionResult> updateUser(MemberUpdateDto memberUpdateDto){
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(username == null) return BadRequest("User not found");
        var user = await userRepository.GetUserByUserNameAsync(username);
        if(user == null) return BadRequest("User not found");
        mapper.Map(memberUpdateDto, user);
        if(await userRepository.SaveAllAsync()) return NoContent();
        return BadRequest("Failed to update user");
    }

}