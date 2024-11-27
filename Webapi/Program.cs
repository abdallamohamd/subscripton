using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Webapi.models;
using Webapi.repo;
namespace Webapi
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
              
            
            builder.Services.AddDbContext<appcontext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });

            builder.Services.AddIdentity<appuser ,IdentityRole>(option =>
            {
                option.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 _";
                
            }).AddEntityFrameworkStores<appcontext>();

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme= JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option => 
            {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["jwt:adu"],
                    ValidateIssuer = true,
                    ValidIssuer= builder.Configuration["jwt:issu"],
                    IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"]))
                };
            });
            //inject
            builder.Services.AddScoped<Iplanrepo, planrepo>();
            builder.Services.AddScoped<Isubsrepo,subsrepo>();


            //cors
                builder.Services.AddCors(option =>
            {
                option.AddPolicy("polcy", polcy =>
                {
                    polcy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });

            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

           
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();                         
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseCors("polcy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
 