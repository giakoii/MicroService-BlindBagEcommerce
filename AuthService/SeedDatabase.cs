using AuthService.Models;
using AuthService.Models.Helpers;
using DotNetEnv;
using OpenIdConnect.Utils.Consts;
using SystemConfig = AuthService.Models.SystemConfig;

namespace AuthService;

public class SeedDatabase
{
    public static void SeedSystemConfig(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!context.SystemConfigs.Any(c => c.Id == "APIKEY_AI"))
        {
            Env.Load();
            
            context.SystemConfigs.AddRange(new[]
            {
                new SystemConfig
                {
                    Id = EnvConst.ApiKeyAi,
                    Value = Environment.GetEnvironmentVariable(EnvConst.ApiKeyAi)!,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "SYSTEM",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "SYSTEM",
                    IsActive = true,
                },
                new SystemConfig
                {
                    Id = EnvConst.EncryptIv,
                    Value = Environment.GetEnvironmentVariable(EnvConst.EncryptIv)!,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "SYSTEM",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "System",
                    IsActive = true,
                },
                new SystemConfig
                {
                    Id = EnvConst.EncryptKey,
                    Value = Environment.GetEnvironmentVariable(EnvConst.EncryptKey)!,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "SYSTEM",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "System",
                    IsActive = true,
                },
                new SystemConfig
                {
                    Id = EnvConst.MailFrom,
                    Value = Environment.GetEnvironmentVariable(EnvConst.MailFrom)!,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "SYSTEM",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "System",
                    IsActive = true,
                },
                new SystemConfig
                {
                    Id = EnvConst.MailSmtp,
                    Value = Environment.GetEnvironmentVariable(EnvConst.MailSmtp)!,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "SYSTEM",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "System",
                    IsActive = true,
                },
                new SystemConfig
                {
                    Id = EnvConst.MailToBcc,
                    Value = Environment.GetEnvironmentVariable(EnvConst.MailToBcc)!,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "SYSTEM",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "System",
                    IsActive = true,
                },
                new SystemConfig
                {
                    Id = EnvConst.MailPassword,
                    Value = Environment.GetEnvironmentVariable(EnvConst.MailPassword)!,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "SYSTEM",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "System",
                    IsActive = true,
                },
                
            });

            context.SaveChanges();
        }
    }
    
    public static void SeedRole(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!context.Roles.Any())
        {
            context.Roles.AddRange(new[]
            {
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Staff",
                    NormalizedName = "STAFF",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "PlannedCustomer",
                    NormalizedName = "PLANNEDCUSTOMER",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                }
            });
            context.SaveChanges();
        }
    }
}