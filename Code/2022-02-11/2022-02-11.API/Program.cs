using _2022_02_11.API.DataAccess.Interfaces;
using _2022_02_11.API.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<_2022_02_11.API.Context._20220211DatabaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("2022-02-11Database")));

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
