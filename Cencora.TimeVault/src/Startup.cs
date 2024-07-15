// Copyright 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Text.Json;
using System.Text.Json.Serialization;
using Cencora.Common.Swagger.Maps;
using Cencora.Common.Swagger.Measurements;
using Cencora.TimeVault.Services.Timezone;
using Cencora.TimeVault.Services.Timezone.TimezoneRetrieval;
using Cencora.TimeVault.Services.Timezone.TimezoneRetrieval.Azure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;

namespace Cencora.TimeVault;

/// <summary>
/// Represents the startup class for the application.
/// </summary>
public class Startup
{
    private IConfiguration Configuration { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <exception cref="ArgumentNullException">KeyVault:KeyVaultUri</exception>
    /// <exception cref="ArgumentNullException">AzureMaps:MapsClientId</exception>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
            });
        services.AddEndpointsApiExplorer();

        // Add Swagger schema filters for the Cencora.Common.Core package.
        services.AddSwaggerGen(options =>
        {
            options.SchemaFilter<DistanceSchemaFilter>();
            options.SchemaFilter<TemperatureSchemaFilter>();
            options.SchemaFilter<VolumeSchemaFilter>();
            options.SchemaFilter<WeightSchemaFilter>();
            options.SchemaFilter<AddressSchemaFilter>();
            
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "OAuth2.0 Authorization Code Flow",
                Name = "oauth2",
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(Configuration["AzureAdSwagger:AuthorizationUrl"] ?? throw new InvalidOperationException()),
                        TokenUrl = new Uri(Configuration["AzureAdSwagger:TokenUrl"] ?? throw new InvalidOperationException()),
                        Scopes = new Dictionary<string, string>
                        {
                            { Configuration["AzureAdSwagger:Scopes"] ?? throw new InvalidOperationException(), "Access the API" }
                        }
                    }
                }
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                    },
                    new[] { Configuration["AzureAdSwagger:Scopes"] ?? throw new InvalidOperationException() }
                }
            });
        });

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

        services.AddSingleton<ITimezoneRetrievalService, AzureTimezoneRetrievalService>();
        services.AddSingleton<ITimeZoneService, RetrievalPipelineTimeZoneService>();
    }

    /// <summary>
    /// Configures the HTTP request pipeline.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <param name="env">The web host environment.</param>
    /// <remarks>
    /// This method is called by the runtime to configure the HTTP request pipeline.
    /// It adds middleware components to the pipeline to handle various aspects of the request processing.
    /// </remarks>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.OAuthClientId(Configuration["AzureAdSwagger:ClientId"]);
                options.OAuthUsePkce();
                options.OAuthScopeSeparator(" ");
            });
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}