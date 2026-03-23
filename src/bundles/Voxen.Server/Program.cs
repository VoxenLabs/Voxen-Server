using FastEndpoints;
using FastEndpoints.Swagger;
using Voxen.Server.Authentication.Extensions;
using Voxen.Server.Domain.Extensions;
using Voxen.Server.Extensions;
using Voxen.Server.Info.Extensions;
using Voxen.Server.Channels.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddVoxenApiServices();
builder.Services.AddVoxenDb();
builder.Services.AddVoxenServerInfo();
builder.Services.AddVoxenChannels();

var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddVoxenAuthentication(jwtSettings);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseFastEndpoints().UseSwaggerGen();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

await app.Services.UseVoxenDb();

await app.RunAsync();
