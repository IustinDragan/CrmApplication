using CRMRealEstate.Application.Helpers.Validators;
using CRMRealEstate.Application.Models.UsersModels;
using CRMRealEstate.Application.Services;
using CRMRealEstate.Application.Services.Interfaces;
using CRMRealEstate.DataAccess.Repositories;
using CRMRealEstate.DataAccess.Repositories.Interfaces;
using CRMRealEstate.DataAccess.Scripts;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Use Bearer YOUR_TOKEN",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
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

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecretKey")),
                ValidateLifetime = true,
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });


        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<DatabaseContext>(optionsBuilder =>
        {
            optionsBuilder.UseNpgsql(connectionString);
        });

        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
        builder.Services.AddScoped<IUsersServices, UsersService>();
        builder.Services.AddScoped<IValidator<CreateUsersRequestModel>, CreateUsersRequestValidator>();
        builder.Services.AddScoped<IUsersRepository, UsersRepository>();
        builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
        builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
        builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
        builder.Services.AddScoped<IPropertyService, PropertyService>();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers().AddJsonOptions(options => { });

        var app = builder.Build();
        //Fix care nu se face:
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}