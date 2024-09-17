using Microsoft.AspNetCore.Mvc.Filters;

namespace API;

public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();
        if(context.HttpContext.User.Identity?.IsAuthenticated != true){
            var userId = context.HttpContext.User.getUserId();
            var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
            var user = await userRepository.GetUserByIdAsync(userId);
            if(user == null) return;
            user.lastActive = DateTime.Now;
            await userRepository.SaveAllAsync();
        }
    }
}