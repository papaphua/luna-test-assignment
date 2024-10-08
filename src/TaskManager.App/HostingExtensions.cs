﻿using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TaskManager.Application.Core;
using TaskManager.Application.Tasks;
using TaskManager.Application.Users;
using TaskManager.Domain.Tasks;
using TaskManager.Domain.Users;
using TaskManager.Persistence;
using TaskManager.Persistence.Tasks;
using TaskManager.Persistence.Users;
using TaskManager.Presentation;

namespace App;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .AddApplicationPart(AssemblyReference.Assembly);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Env.GetString("JWT_ISSUER"),
                    ValidAudience = Env.GetString("JWT_AUDIENCE"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("JWT_KEY")))
                };
            });

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddScoped<IUnitOfWork>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ITaskRepository, TaskRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ITaskService, TaskService>();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers()
            .RequireAuthorization();

        app.Run();

        return app;
    }
}