using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Voxen.Server;
using Voxen.Server.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();

builder.Services.AddSwaggerGen();
builder.Services.SwaggerDocument(o =>
{
    o.ShortSchemaNames = true;
    o.DocumentSettings = s =>
    {
        s.Title = "Voxen Server API";
        s.Version = "v1";
    };
});

var dbPath = Environment.GetEnvironmentVariable("VOXEN_DB_PATH") ?? "data/voxen.db";
builder.Services.AddDbContext<VoxenDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

builder.Services
    .AddIdentityCore<User>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<VoxenDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),

            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<VoxenDbContext>();
await db.Database.MigrateAsync();

app.MapControllers();

await app.RunAsync();
