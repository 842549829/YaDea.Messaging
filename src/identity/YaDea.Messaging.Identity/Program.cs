
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

            // ע��Controller
            services.AddControllers();

            // ע�����ݿ�DB
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // ע��Identity
            //services.AddIdentity<AppUser, AppRole>(options =>
            //{
            //    // �����������
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //}).AddEntityFrameworkStores<AppDbContext>();
            services.AddIdentityCore<AppUser>(options =>
            {
                // �����������
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>();

            // ע��JWT����
            var configurationSectionJwtSettingOptions = configuration.GetSection(nameof(JwtSettingOptions));
            services.Configure<JwtSettingOptions>(configurationSectionJwtSettingOptions);
            var jwtSettings = configurationSectionJwtSettingOptions.Get<JwtSettingOptions>();

            // ע�������֤
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

            // ע���û�����
            services.AddScoped<IUserService, UserService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ·��
            app.UseRouting();

            // �����֤
            app.UseAuthentication();

            // ��Ȩ
            app.UseAuthorization();

            // Controller
            app.MapControllers();

            app.Run();
        }
    }
}