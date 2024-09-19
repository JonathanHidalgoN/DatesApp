using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options){
    public DbSet<AppUser> Users{get; set;}
    public DbSet<UserLike> Likes{get; set;}

    protected override void OnModelCreating(ModelBuilder builder){
        base.OnModelCreating(builder);

        builder.Entity<UserLike>()
            .HasKey(key => new {key.sourceUserId, key.targetUserId});

        builder.Entity<UserLike>()
            .HasOne(source => source.sourceUser)
            .WithMany(like => like.LikedUsers)
            .HasForeignKey(key => key.sourceUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserLike>()
            .HasOne(source => source.targetUser)
            .WithMany(like => like.LikedByUsers)
            .HasForeignKey(key => key.targetUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}