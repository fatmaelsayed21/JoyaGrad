
using System;
using System.Text;
using Domain.Contracts;
using Domain.Models;
using Joya.Api.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persentation;
using Persistence.Data;
using Persistence.Repositories;

namespace Joya.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<JoyaDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); 

            builder.Services.AddIdentity<User,IdentityRole>()
                .AddEntityFrameworkStores<JoyaDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) 
                .AddJwtBearer(options =>
                      {
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                                 ValidateIssuer = true,
                                 ValidateAudience = true,
                                 ValidateLifetime = true,
                                 ValidateIssuerSigningKey = true,
                                 ValidIssuer = "Joya.com",
                                 ValidAudience = "Joya.com", 
                                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("s7b@1X!z4eW#9rLpQzVt3$YgMnKx2#Hv")) // secret key
                         };
                });

            builder.Services.Configure<EmailSettings>(
            builder.Configuration.GetSection("EmailSettings"));

            builder.Services.AddScoped<EmailService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

           
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IVenueService, VenueService>();
            builder.Services.AddScoped<IDecorationService, DecorationService>();
            builder.Services.AddScoped<IMusicEnvironment, MusicEnvironmentService>();
            builder.Services.AddScoped<IPhotographyAndVideogrpahy, PhotographyAndVideographyService>();
            builder.Services.AddScoped<BookingService, BookingService>();

            builder.Services.AddMemoryCache();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            app.UseHttpsRedirection();


            app.UseAuthentication();

            app.UseCors("AllowAll");
            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
