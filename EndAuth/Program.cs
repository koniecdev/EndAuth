using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using EndAuth.Persistance;
using Microsoft.Extensions.DependencyInjection.Extensions;
using EndAuth.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(m =>
{
    m.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    m.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }

    });
});

var config = builder.Configuration;
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowedPolicies", corsBuilder =>
//    {
//        corsBuilder.WithOrigins("https://localhost:7074").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
//    });
//});


builder.Services.AddAuthentication(m =>
{
    m.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    m.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    m.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(m =>
{
    m.SaveToken = true;
    m.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true, 
        ValidateIssuerSigningKey = true
    };
});

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuthorization();
builder.Services.AddJwtProvider();
builder.Services.AddApplication();
builder.Services.AddPersistance(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
//app.UseCors("AllowedPolicies");

app.UseAuthorization();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
