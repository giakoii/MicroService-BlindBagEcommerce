using System.Net;
using AccessoryService.Logics;
using AccessoryService.Models.Helper;
using AccessoryService.Repositories;
using AccessoryService.Services;
using AccessoryService.SystemClient;
using AccessoryService.Utils.Const;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using NSwag;
using NSwag.Generation.Processors.Security;
using OpenIddict.Validation.AspNetCore;
using OpenApiSecurityScheme = NSwag.OpenApiSecurityScheme;

var builder = WebApplication.CreateBuilder(args);

// Configuration
Env.Load();

// Get the connection string from environment variables
var connectionString = Environment.GetEnvironmentVariable(EnvConst.ConnectionString);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<CloudinaryLogic>();

builder.Services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
builder.Services.AddScoped<IAccessoryRepository, AccessoryRepository>();
builder.Services.AddScoped<IAccessoryService, AccessoryService.Services.AccessoryService>();
builder.Services.AddScoped<IIdentityApiClient, IdentityApiClient>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


// Add services to the container.
builder.Services.AddControllers();

// SignalR Service
builder.Services.AddSignalR();
// Swagger configuration to output API type definitions
builder.Services.AddOpenApiDocument(config =>
{
    config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
    config.AddSecurity("JWT Token", Enumerable.Empty<string>(),
        new OpenApiSecurityScheme()
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = nameof(Authorization),
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Copy this into the value field: Bearer {token}"
        }
    );
});

// Allow API to be read from outside
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
    );
});

// Use AppDbContext that inherits DB context
builder.Services.AddDbContext<AppDbContext>();

// Configure the authentication server for the API
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
});


// Configure the OpenIddict server
builder.Services.AddOpenIddict()
    .AddValidation(options =>
    {
        options.SetIssuer("http://localhost:5079/");
        options.AddAudiences(SystemConfig.ServiceClient);

        options.UseIntrospection()
            .AddAudiences(SystemConfig.ServiceClient)
            .SetClientId(SystemConfig.ServiceClient)
            .SetClientSecret(Environment.GetEnvironmentVariable(EnvConst.ClientSecret)!);

        options.UseSystemNetHttp();
        options.UseAspNetCore();
    });

// DB context that inherits AppDbContext
builder.Services.AddHttpContextAccessor();

// ConfigureServices
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseDeveloperExceptionPage(); 
app.UseStatusCodePages(); 
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.UseOpenApi();
app.UseSwaggerUi();
app.Run();