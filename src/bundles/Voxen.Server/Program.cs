using FastEndpoints;
using FastEndpoints.Swagger;
using Voxen.Server.Domain.Extensions;
using Voxen.Server.Extensions;
using Voxen.Server.Interfaces;
using Voxen.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddVoxenApiServices();
builder.Services.AddVoxenDb();

var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddVoxenAuthentication(jwtSettings);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IServerConfigurationProvider, ServerConfigurationProvider>();

var app = builder.Build();

app.UseFastEndpoints().UseSwaggerGen();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

await app.Services.UseVoxenDb();

await app.RunAsync();
