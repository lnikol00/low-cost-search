using Microsoft.EntityFrameworkCore;
using TravelApp.Models.Configuration;
using TravelApp.Services;
using TravelApp.Services.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.Configure<ConnectionApi>(builder.Configuration.GetSection("ConnectionApi"));

builder.Services.AddScoped<IAmadeusService, AmadeusService>();

builder.Services.AddCors(p => p.AddPolicy("cors_policy_allow_all", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSession();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors("cors_policy_allow_all");

app.MapControllers();

app.Run();
