namespace API;

public interface ITokenService{
    /**
     * This method is used to create a token
     * @param user: The user object
     * @return The token
     */
    string createToken(AppUser user);
}