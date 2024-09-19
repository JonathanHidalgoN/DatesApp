using API;

public interface ILikesRepository{

    /*
    * This method is used to get a user like
    * @param sourceUserId: int
    * @param targetUserId: int
    */
    Task<UserLike?> getUserLike(int sourceUserId, int targetUserId);

    /*
    * This method is used to get all user likes
    * @param predicate: string
    * @param userId: int
    */
    Task<IEnumerable<MembersDto>> getUserLikes(string predicate, int userId);

    /*
    * This method is used to get the current user like ids
    * @param currentUserId: int
    */
    Task<IEnumerable<int>> getCurrentUserLikeIds(int currentUserId);

    /*
    * This method is used to delete a user like
    * @param like: UserLike
    */
    void deleteLike(UserLike like);

    /*
    * This method is used to add a user like
    * @param like: UserLike
    */
    void addLike(UserLike like);

    /*
    * This method is used to save changes
    */
    Task<bool> saveChanges();
}