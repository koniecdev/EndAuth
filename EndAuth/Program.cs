using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using EndAuth.Persistance;
using Microsoft.Extensions.DependencyInjection.Extensions;
using EndAuth.Shared;
using EndAuth.JwtProvider;
using EndAuth.JwtProvider.TokenParameterFactory;
using Serilog;
using EndAuth.Infrastructure.Extensions;
using EndAuth.Application;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
Log.Information("Starting up");

try
{
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

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowedPolicies", corsBuilder =>
        {
            corsBuilder.WithOrigins("https://localhost:7171").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            corsBuilder.WithOrigins("https://localhost:7017").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
    });

    builder.Services.AddAuthentication(m =>
    {
        m.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        m.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        m.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(m =>
    {
        m.SaveToken = true;
        m.TokenValidationParameters = TokenParametersFactory.Create(builder.Configuration);
    });

    builder.Services.AddAuthorization();

    builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    builder.Services.AddJwtProvider();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();
    builder.Services.AddPersistance(builder.Configuration);

    var app = builder.Build();

    app.UseExceptionsHandling();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseRouting();
    app.UseCors("AllowedPolicies");

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

