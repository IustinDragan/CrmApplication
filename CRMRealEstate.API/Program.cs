using CRMRealEstate.Application.Helpers.Validators;
using CRMRealEstate.Application.Models.UsersModels;
using CRMRealEstate.Application.Services;
using CRMRealEstate.Application.Services.Interfaces;
using CRMRealEstate.DataAccess.Repositories;
using CRMRealEstate.DataAccess.Repositories.Interfaces;
using CRMRealEstate.DataAccess.Scripts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DatabaseContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(connectionString);  
});

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IUsersServices, UsersService>();
builder.Services.AddScoped<IValidator<CreateUsersRequestModel>, CreateUsersRequestValidator>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

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