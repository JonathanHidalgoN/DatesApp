using API.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API;

public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
{
    
    public async Task<MembersDto?> GetMemberAsync(string username){
        return await context.Users.Where(x => x.username == username)
        .ProjectTo<MembersDto>(mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task<PagedList<MembersDto>> GetMembersAsync(UserParams userParam){
        var query = context.Users.AsQueryable();
        query = query.Where(x => x.username != userParam.currentUsername);
        if(userParam.gender != null){
            query = query.Where(x => x.gender == userParam.gender);
        }

        var minDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParam.MaxAge-1));
        var maxDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParam.MinAge));

        query = query.Where(x => x.dateOfBirth >= minDob && x.dateOfBirth <= maxDob);

        query = userParam.orderBy switch
        {
            "created" => query.OrderByDescending(x => x.created),
            _ => query.OrderByDescending(x => x.lastActive)
            
        };

        return await PagedList<MembersDto>.createAsync(query.ProjectTo<MembersDto>(mapper.ConfigurationProvider)
        , userParam.PageNumber, userParam.PageSize);
    }

    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<AppUser?> GetUserByUserNameAsync(string username)
    {
        return await context.Users.Include(x => x.Photos)
        .SingleOrDefaultAsync(x => x.username == username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await context.Users.Include(x => x.Photos)
        .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Update(AppUser user)
    {
        context.Entry(user).State = EntityState.Modified;
    }
   
}