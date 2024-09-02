using API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

public class UsersController(DataContext context) : BaseApiController{       
    /*
    Get all the users from the database
    */
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> getUsers(){
        var users = await context.Users.ToListAsync();
        return users;
    }

    /*
    Get just one user
    param id : Id of the user to fetch
    */
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppUser>> getUser(int id){
        var user = await context.Users.FindAsync(id);
        if(user == null){
            return NotFound();
        }

        return user;
    }

}