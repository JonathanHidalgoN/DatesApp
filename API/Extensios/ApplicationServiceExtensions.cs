using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API;

public static class ApplicationServiceExtensions{
    
    /**
        * This method is used to add services to the application
        * @param services: IServiceCollection
        * @param config: IConfiguration
        * @return IServiceCollection
    */
    public static IServiceCollection AddApplicationService(this IServiceCollection services,
        IConfiguration config){
            services.AddControllers();
            services.AddDbContext<DataContext>(opt =>{
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<LogUserActivity>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        return services;
        }
}