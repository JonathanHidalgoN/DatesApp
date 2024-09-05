using API.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API;

public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
{
    public async Task<MembersDto?> GetMemberAsync(string username){
        return await context.Users.Where(x => x.userName == username)
        .ProjectTo<MembersDto>(mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MembersDto>> GetMembersAsync(){
        return await context.Users
        .ProjectTo<MembersDto>(mapper.ConfigurationProvider)
        .ToListAsync();
    }

    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<AppUser?> GetUserByUserNameAsync(string userName)
    {
        return await context.Users.Include(x => x.Photos)
        .SingleOrDefaultAsync(x => x.userName == userName);
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