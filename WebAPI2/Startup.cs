using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI2.Entities;
using WebAPI2.Services;
using WebAPI2.Middleware;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using WebAPI2.Models.Validators;
using WebAPI2.Models;
using FluentValidation.AspNetCore;
//using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.IdentityModel.Tokens;

namespace WebAPI2
{
    
    public class Startup
    {
        public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        var authenticationSettings = new AuthenticationSettings();

        Configuration.GetSection("Authetication").Bind(authenticationSettings);

        services.AddSingleton(authenticationSettings);
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = "Bearer";
            option.DefaultScheme = "Bearer";
            option.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = authenticationSettings.JwtIssuer,
                ValidAudience = authenticationSettings.JwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
            };
        });
        services.AddAuthorization(options => {
            options.AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality"));
            });
        services.AddControllers().AddFluentValidation();
        services.AddDbContext<RestaurantDbContext>();
        services.AddScoped<RestaurantSeeder>();
        services.AddAutoMapper(this.GetType().Assembly);
        services.AddScoped<IRestaurantService, RestaurantService>();
        
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ErrorHandlingMiddleware>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
        services.AddScoped<RequestTimeMiddleware>();
        services.AddSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RestaurantSeeder seeder)
    {
        seeder.Seed();
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseMiddleware<RequestTimeMiddleware>();
        app.UseAuthentication();
        app.UseHttpsRedirection();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API");
        });

        app.UseRouting();
            app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
    //public class Startup
    //{
    //    public Startup(IConfiguration configuration)
    //    {
    //        Configuration = configuration;
    //    }

    //    public IConfiguration Configuration { get; }

    //    // This method gets called by the runtime. Use this method to add services to the container.
    //    public void ConfigureServices(IServiceCollection services)
    //    {
    //        var autheticationSettings = new AuthenticationSettings();
    //        Configuration.GetSection("Authentication").Bind(autheticationSettings);
    //        services.AddSingleton(autheticationSettings);
    //        services.AddAuthentication(option =>
    //        {
    //            option.DefaultAuthenticateScheme = "Bearer";
    //            option.DefaultScheme = "Bearer";
    //            option.DefaultChallengeScheme = "Bearer";
    //        }).AddJwtBearer(cfg =>
    //        {
    //            cfg.RequireHttpsMetadata = false;
    //            cfg.SaveToken = true;
    //            cfg.TokenValidationParameters = new TokenValidationParameters
    //            {
    //                ValidateIssuer = true,//wydawca
    //                ValidateAudience = true,//podmioty
    //                //klucz prywatny
    //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(autheticationSettings.JwtKey)),
    //            };
    //        });
    //        services.AddControllers().AddFluentValidation();
    //        services.AddDbContext<RestaurantDbContext>();
    //        services.AddScoped<RestaurantSeeder>();
    //        services.AddAutoMapper(this.GetType().Assembly);
    //        services.AddScoped<IRestaurantService, RestaurantService>();
    //        services.AddScoped<ErrorHandlingMiddleware>();
    //        services.AddScoped<RequestTimeMiddleware>();
    //        services.AddSwaggerGen();
    //        services.AddScoped<IAccountService, AccountService >();
    //        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    //        services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();

    //    }

    //    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    //    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RestaurantSeeder seeder)
    //    {
    //        seeder.Seed();
    //        if (env.IsDevelopment())
    //        {
    //            app.UseDeveloperExceptionPage();
    //        }
    //        app.UseMiddleware<ErrorHandlingMiddleware>();
    //        app.UseMiddleware<RequestTimeMiddleware>();
    //        app.UseAuthentication();
    //        app.UseHttpsRedirection();

    //        app.UseSwagger();
    //        app.UseSwaggerUI(c =>
    //        {
    //            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API");
    //        });
    //        app.UseRouting();

    //        app.UseAuthorization();

    //        app.UseEndpoints(endpoints =>
    //        {
    //            endpoints.MapControllers();
    //        });
    //    }
    //}
}
