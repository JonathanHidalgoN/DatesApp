using Microsoft.AspNetCore.Mvc;

namespace API;

public class LikesController(ILikesRepository likesRepository): BaseApiController
{
    /*
    * This method toggles a like between the current user and the target user
    * @param targetUserId: The id of the user to like
    * @returns Ok if the like was toggled successfully, BadRequest otherwise
    */
    [HttpPost("{targetUserId:int}")]
    public async Task<ActionResult> toggleLike(int targetUserId){
        var sourceUserId = User.getUserId();
        if(sourceUserId == targetUserId) return BadRequest("You cannot like yourself");
        var existingLike = await likesRepository.getUserLike(sourceUserId, targetUserId);
        if(existingLike == null){
            var like = new UserLike{
                sourceUserId = sourceUserId,
                targetUserId = targetUserId
            };
            likesRepository.addLike(like);
        }
        else{
            likesRepository.deleteLike(existingLike);
        }
        if(await likesRepository.saveChanges()) return Ok();
        return BadRequest("Failed to like user");
    }

    /*
    * This method returns a list of user ids that the current user has liked
    * @returns Ok with the list of user ids
    */
    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<int>>> getCurrentUserLikeIds(){
        return Ok(await likesRepository.getCurrentUserLikeIds(User.getUserId()));
    }

    /*
    * This method returns a list of users that the current user has liked
    * @param predicate: The type of likes to return (liked, likedBy, default)
    * @returns Ok with the list of users
    */
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembersDto>>> getUserLikes(string predicate){
        var users = await likesRepository.getUserLikes(predicate, User.getUserId());
        return Ok(users);
    }

}