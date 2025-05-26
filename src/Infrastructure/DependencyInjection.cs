using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Domain.Constants;
using PokemonInHomeAPI.Infrastructure.Data;
using PokemonInHomeAPI.Infrastructure.Data.Interceptors;
using PokemonInHomeAPI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("PokemonInHomeAPIDb");
        Guard.Against.Null(connectionString, message: "Connection string 'PokemonInHomeAPIDb' not found.");

        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString).AddAsyncSeeding(sp);
        });


        builder.Services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        builder.Services.AddScoped<ApplicationDbContextInitialiser>();

        builder.Services.AddAuthentication(options =>
        {
                options.DefaultScheme = IdentityConstants.BearerScheme;
                options.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
                options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
        }).AddBearerToken(IdentityConstants.BearerScheme);

        
        // builder.Services.AddAuthentication(options =>
        // {
        //         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //         options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        // }).AddJwtBearer(options =>
        // {
        //     options.TokenValidationParameters = new TokenValidationParameters
        //     {
        //         ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //         ValidAudience = builder.Configuration["Jwt:Audience"],
        //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        //         ValidateIssuer = true,
        //         ValidateAudience = true,
        //         ValidateLifetime = true,
        //         ValidateIssuerSigningKey = true
        //     };
        // }).AddBearerToken(IdentityConstants.BearerScheme);

        builder.Services.AddAuthorizationBuilder();

        builder.Services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddTransient<IIdentityService, IdentityService>();

        builder.Services.AddAuthorization(options =>
        {
            // Default Policy
            options.AddPolicy(Policies.User, policy => policy.RequireRole(Roles.User));
            options.AddPolicy(Policies.Administrator, policy => policy.RequireRole(Roles.Administrator));
            options.AddPolicy(Policies.Player, policy => policy.RequireRole(Roles.Player));
            
            // Spesific Policy
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator));
        });
    }
}
