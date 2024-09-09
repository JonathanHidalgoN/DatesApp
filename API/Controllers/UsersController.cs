using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService) : BaseApiController{       
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
        var user = await userRepository.GetUserByUserNameAsync(User.getUsername());
        if(user == null) return BadRequest("User not found");
        mapper.Map(memberUpdateDto, user);
        if(await userRepository.SaveAllAsync()) return NoContent();
        return BadRequest("Failed to update user");
    }

    /*
    Method to add a photo to the user
    param file : The photo to add
    */
    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> addPhoto(IFormFile file){
        var user = await userRepository.GetUserByUserNameAsync(User.getUsername());
        if(user == null) return BadRequest("User not found");
        var result = await photoService.AddPhotoAsync(file);
        if(result.Error != null) return BadRequest(result.Error.Message);
        var photo = new Photo{
            url = result.SecureUrl.AbsoluteUri,
            publicId = result.PublicId
        };
        user.Photos.Add(photo);
        if(await userRepository.SaveAllAsync()) 
            return CreatedAtAction(nameof(getUser), 
            new {username = user.username}, mapper.Map<PhotoDto>(photo));
        return BadRequest("Problem adding photo");
    }

    /*
    Method to delete a photo from the user
    param photoId : The id of the photo to delete
    */
    [HttpPut("set-main-photo/{photoId:int}")]
    public async Task<ActionResult> setMainPhoto(int photoId){
        var user = await userRepository.GetUserByUserNameAsync(User.getUsername());
        if(user == null) return BadRequest("User not found");
        var photo = user.Photos.FirstOrDefault(x => x.id == photoId);
        if(photo == null || photo.isMain) return BadRequest("Photo not found or already main");
        var currentMain = user.Photos.FirstOrDefault(x => x.isMain);
        if(currentMain != null) currentMain.isMain = false;
        photo.isMain = true;
        if(await userRepository.SaveAllAsync()) return NoContent();
        return BadRequest("Failed to set main photo");

    }

    /*
    Method to delete a photo from the user
    param photoId : The id of the photo to delete
    */
    [HttpDelete("delete-photo/{photoId:int}")]
    public async Task<ActionResult> deletePhoto(int photoId){
        var user = await userRepository.GetUserByUserNameAsync(User.getUsername());
        if(user == null) return BadRequest("User not found");
        var photo = user.Photos.FirstOrDefault(x => x.id == photoId);
        if(photo == null) return NotFound();
        if(photo.isMain) return BadRequest("You cannot delete your main photo");
        if(photo.publicId != null){
            var result = await photoService.DeletePhotoAsync(photo.publicId);
            if(result.Error != null) return BadRequest(result.Error.Message);
        }
        user.Photos.Remove(photo);
        if(await userRepository.SaveAllAsync()) return Ok();
        return BadRequest("Failed to delete the photo");
    } 

}