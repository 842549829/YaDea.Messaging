using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace YaDea.Messaging.Identity.Data
{
    /*
     * 数据库迁移命令
     *dotnet ef migrations add init -c AppDbContext
     *dotnet ef database update -c AppDbContext
     *
     */
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(b => { b.ToTable("AppUsers"); });

            builder.Entity<AppRole>(b => { b.ToTable("AppUserRoles"); });

            builder.Entity<IdentityUserClaim<Guid>>(b => { b.ToTable("AppUserClaims"); });

            builder.Entity<IdentityUserLogin<Guid>>(b => { b.ToTable("AppUserLogins"); });

            builder.Entity<IdentityUserToken<Guid>>(b => { b.ToTable("AppUserTokens"); });

            builder.Entity<IdentityRole<Guid>>(b => { b.ToTable("AppRoles"); });

            builder.Entity<IdentityRoleClaim<Guid>>(b => { b.ToTable("AppRoleClaims"); });

            builder.Entity<RefreshToken>(b => { b.ToTable("AppRefreshToken"); });
        }
    }
}