using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Voxen.Server;
using Voxen.Server.Entities;
using Voxen.Server.Extensions;
using Voxen.Server.Interfaces;
using Voxen.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddVoxenApiServices();

var dbPath = Environment.GetEnvironmentVariable("VOXEN_DB_PATH") ?? "voxen.db";
builder.Services.AddDbContext<VoxenDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddVoxenAuthentication(jwtSettings);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IServerConfigurationProvider, ServerConfigurationProvider>();

var app = builder.Build();

app.UseFastEndpoints().UseSwaggerGen();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<VoxenDbContext>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
await SeedData.InitializeDatabaseAsync(db, userManager);

await app.RunAsync();
