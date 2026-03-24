using FastEndpoints;
using FastEndpoints.Swagger;
using Voxen.Server.Authentication.Extensions;
using Voxen.Server.Channels.Extensions;
using Voxen.Server.Domain.Extensions;
using Voxen.Server.Extensions;
using Voxen.Server.Info.Extensions;
using Voxen.Server.Channels.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpContextAccessor();

var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services
    .AddVoxenApiServices()
    .AddVoxenAuthentication(jwtSettings)
    .AddVoxenChannels()
    .AddVoxenDb()
    .AddVoxenServerInfo();

var app = builder.Build();

app
    .UseFastEndpoints()
    .UseSwaggerGen()
    .UseStaticFiles();

app.UseVoxenAuthentication();
await app.Services.UseVoxenDb();

await app.RunAsync();
