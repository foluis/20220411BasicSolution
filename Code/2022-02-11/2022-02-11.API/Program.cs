
using _2022_02_11.Entities.Context;
using _2022_02_11.Entities.Models;
using _2022_02_11.Infrastructure;
using _2022_02_11.Repositories;
using _2022_02_11.Services;
using _20220211.Identity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<_20220211DatabaseContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("2022-02-11Database")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDatabase")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
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

builder.Services.AddScoped(sp =>
{
    var httpContext = sp.GetService<IHttpContextAccessor>().HttpContext;

    var identityOptions = new _2022_02_11.Infrastructure.IdentityOptions();

    if (httpContext.User.Identity.IsAuthenticated)
    {
        string id = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        string firstName = httpContext.User.FindFirst(ClaimTypes.GivenName).Value;
        string lastName = httpContext.User.FindFirst(ClaimTypes.Surname).Value;
        string email = httpContext.User.FindFirst(ClaimTypes.Email).Value;
        string role = httpContext.User.FindFirst(ClaimTypes.Role).Value;

        identityOptions.UserId = id;
        identityOptions.Email = email;
        identityOptions.FullName = $"{firstName} {lastName}";
        identityOptions.IsAdmin = role == "Admin" ? true : false;
    }

    return identityOptions;
});

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUsersProfileService, UsersProfileService>();

builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
