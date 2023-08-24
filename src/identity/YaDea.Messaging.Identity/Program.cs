
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using YaDea.Messaging.Identity.Data;
using YaDea.Messaging.Identity.Options;
using YaDea.Messaging.Identity.Services;

namespace YaDea.Messaging.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;

            // 注册Controller
            services.AddControllers();

            // 注册数据库DB
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // 注册Identity
            //services.AddIdentity<AppUser, AppRole>(options =>
            //{
            //    // 配置密码规则
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //}).AddEntityFrameworkStores<AppDbContext>();
            services.AddIdentityCore<AppUser>(options =>
            {
                // 配置密码规则
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>();

            // 注册JWT配置
            var configurationSectionJwtSettingOptions = configuration.GetSection(nameof(JwtSettingOptions));
            services.Configure<JwtSettingOptions>(configurationSectionJwtSettingOptions);
            var jwtSettings = configurationSectionJwtSettingOptions.Get<JwtSettingOptions>();

            // 注册身份认证
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecurityKey)),
                        ClockSkew = TimeSpan.Zero,
                    };
                });

            // Add services to the container.
            services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // 注册用户服务
            services.AddScoped<IUserService, UserService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // 路由
            app.UseRouting();

            // 身份认证
            app.UseAuthentication();

            // 授权
            app.UseAuthorization();

            // Controller
            app.MapControllers();

            app.Run();
        }
    }
}