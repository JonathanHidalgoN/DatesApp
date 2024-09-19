
using API.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API;

public class LikesRepository(DataContext context, IMapper mapper) : ILikesRepository
{
    public void addLike(UserLike like)
    {
        context.Likes.Add(like);
    }

    public void deleteLike(UserLike like)
    {
        context.Likes.Remove(like);
    }

    public async Task<IEnumerable<int>> getCurrentUserLikeIds(int currentUserId)
    {
        return await context.Likes
        .Where(x => x.sourceUserId == currentUserId)
        .Select(x => x.targetUserId)
        .ToListAsync();
    }

    public async Task<UserLike?> getUserLike(int sourceUserId, int targetUserId)
    {
        return await context.Likes.FindAsync(sourceUserId, targetUserId);
    }

    public async Task<IEnumerable<MembersDto>> getUserLikes(string predicate, int userId)
    {
        var likes = context.Likes.AsQueryable();
        switch (predicate){
            case "liked":
                return await likes.Where(x => x.sourceUserId == userId)
                .Select(x => x.targetUser )
                .ProjectTo<MembersDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            case "likedBy":
                return await likes.Where(x => x.targetUserId == userId)
                .Select(x => x.sourceUser)
                .ProjectTo<MembersDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            default:
                var likeIds = await getCurrentUserLikeIds(userId);
                return await likes.Where(x=>x.targetUserId == userId && likeIds.Contains(x.sourceUserId))
                .Select(x => x.sourceUser)
                .ProjectTo<MembersDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }

    public async Task<bool> saveChanges()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
