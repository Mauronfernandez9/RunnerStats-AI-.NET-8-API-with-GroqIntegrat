using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RunnerStats.Data.Context;
using RunnerStats.Helpers;
using RunnerStats.Data.Repositories;
using RunnerStats.Data.Implementations;
using RunnerStats.Services.Implementations;
using RunnerStats.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RunnerStatsContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
    });
builder.Services.AddSingleton<Utilities>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IRunnerRepository,RunnerRepository>();
builder.Services.AddScoped<IRunnerService,RunnerService>();
builder.Services.AddScoped<INutritionRepository,NutritionRepository>();
builder.Services.AddScoped<INutritionService,NutritionService>();

builder.Services.AddScoped<IGroqApiClient>(sp => new GroqApiClient(builder.Configuration["Groq:apiKey"]!));

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]!))
    };
});

builder.Services.AddCors(options => options.AddPolicy("NewPolicy", app =>
{
    app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NewPolicy");

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
