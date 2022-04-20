using _2022_02_11.API.Context;
using _2022_02_11.API.DataAccess.Interfaces;
using _2022_02_11.API.DataAccess.Repositories;
using _2022_02_11.Entities.DataSeeding;
using _2022_02_11.Infrastructure;
using _2022_02_11.Repositories;
using _20220211.Identity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

//using Microsoft.IdentityModel.Tokens;
//using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<_20220211DatabaseContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("2022-02-11Database")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDatabase")));



//add a reference to the Microsoft.AspNetCore.Identity.UI nuget package
//builder.Services.AddDefaultIdentity<IdentityUser>(
//    options => options.SignIn.RequireConfirmedAccount = true)
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<_20220211IdentityContext>();

/***********************************************/


//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
//{
//    options.User.RequireUniqueEmail = false;
//}).AddEntityFrameworkStores<_20220211IdentityContext>()
//    .AddDefaultTokenProviders();

/***********************************************/

//builder.Services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<_20220211IdentityContext>();


//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Password settings.
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequiredUniqueChars = 1;

//    // Lockout settings.
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;

//    // User settings.
//    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//    options.User.RequireUniqueEmail = false;
//});


/***********************************************/


builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();



builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AuthSettings:Audience"],
        ValidIssuer = builder.Configuration["AuthSettings:Issuer"],
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"])),
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();

builder.Services.AddScoped(sp => new AuthOptions
{
    Audience = builder.Configuration["AuthSettings:Audience"],
    Issuer = builder.Configuration["AuthSettings:Issuer"],
    Key = builder.Configuration["AuthSettings:Key"]
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();


builder.Services.AddControllers();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
