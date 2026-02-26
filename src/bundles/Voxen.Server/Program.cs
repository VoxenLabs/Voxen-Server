using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Voxen.Server;
using Voxen.Server.Authentication.Extensions;
using Voxen.Server.Entities;
using Voxen.Server.Interfaces;
using Voxen.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(o =>
{
    o.ShortSchemaNames = true;
    o.DocumentSettings = s =>
    {
        s.Title = "Voxen Server API";
        s.Version = "v1";
    };
});

var dbPath = Environment.GetEnvironmentVariable("VOXEN_DB_PATH") ?? "voxen.db";
builder.Services.AddDbContext<VoxenDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

builder.Services
    .AddIdentityCore<User>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = false;
        options.User.RequireUniqueEmail = false;
    })
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<VoxenDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddVoxenAuthentication(jwtSettings);
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IServerConfigurationProvider, ServerConfigurationProvider>();

var app = builder.Build();

app.UseFastEndpoints().UseSwaggerGen();
app.UseStaticFiles();

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<VoxenDbContext>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
await SeedData.InitializeDatabaseAsync(db, userManager);

await app.RunAsync();
