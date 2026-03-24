using FastEndpoints;
using FastEndpoints.Swagger;
using Voxen.Server.Authentication.Extensions;
using Voxen.Server.Domain.Extensions;
using Voxen.Server.Extensions;
using Voxen.Server.Info.Extensions;
using Voxen.Server.Channels.Extensions;

var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("Jwt");

// referenced projects
builder.Services
    .AddVoxenApiServices()
    .AddVoxenAuthentication(jwtSettings)
    .AddVoxenChannels();
    .AddVoxenDb()
    .AddVoxenServerInfo()

// generic services
builder.Services
    .AddHttpContextAccessor();

var app = builder.Build();

app
    .UseFastEndpoints()
    .UseSwaggerGen()
    .UseStaticFiles()
    .UseAuthentication()
    .UseAuthorization();

// referenced projects
await app.Services.UseVoxenDb();

await app.RunAsync();
