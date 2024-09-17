namespace API;

public interface IUserRepository{

    /**
    * Get all users
    * @return A list of all users
    */
    void Update(AppUser user);

    /**
    * Update a user
    * @param user The user to update
    */
    Task<bool> SaveAllAsync();

    /**
    * Save all changes to the database
    * @return True if changes were saved, false otherwise
    */
    Task<IEnumerable<AppUser>> GetUsersAsync();

    /**
    * Get all users
    * @return A list of all users
    */
    Task<AppUser?> GetUserByIdAsync(int id);

    /**
    * Get a user by their ID
    * @param id The ID of the user
    * @return The user with the given ID
    */
    Task<AppUser?> GetUserByUserNameAsync(string username);

    /**
    * Get a user by their username
    * @param username The username of the user
    * @return The user with the given username
    */
    Task<PagedList<MembersDto>> GetMembersAsync(UserParams userParams);

    /**
    * Get all members
    * @return A list of all members
    */
    Task<MembersDto?> GetMemberAsync(string username);
}