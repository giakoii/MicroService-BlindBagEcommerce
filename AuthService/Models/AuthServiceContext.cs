using Microsoft.EntityFrameworkCore;

namespace AuthService.Models;

public partial class AuthServiceContext : DbContext
{
    public AuthServiceContext()
    {
    }

    public AuthServiceContext(DbContextOptions<AuthServiceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<InformationUser> InformationUsers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SystemConfig> SystemConfigs { get; set; }

    public virtual DbSet<User> Users { get; set; }
    
    public virtual DbSet<VwEmailTemplateVerifyOtp> VwEmailTemplateVerifyOtps { get; set; }

    public virtual DbSet<VwEmailTemplateVerifyUser> VwEmailTemplateVerifyUsers { get; set; }

    public virtual DbSet<VwRole> VwRoles { get; set; }

    public virtual DbSet<VwUserAuthentication> VwUserAuthentications { get; set; }

    public virtual DbSet<VwUserLogin> VwUserLogins { get; set; }

    public virtual DbSet<VwUserVerify> VwUserVerifies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            DotNetEnv.Env.Load(); 

            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Missing CONNECTION_STRING environment variable");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.UseOpenIddict();

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Address__CAA247C8F597A43D");

            entity.ToTable("Address");

            entity.Property(e => e.AddressId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("address_id");
            entity.Property(e => e.AddressLine)
                .HasMaxLength(255)
                .HasColumnName("address_line");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.District)
                .HasMaxLength(50)
                .HasColumnName("district");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Province).HasColumnName("province");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.Ward)
                .HasMaxLength(50)
                .HasColumnName("ward");
        });

        modelBuilder.Entity<EmailTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__email_te__3213E83F83E83DA0");

            entity.ToTable("EmailTemplate");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.CreateAt)
                .HasPrecision(6)
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("create_by");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ScreenName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("screen_name");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.UpdateAt)
                .HasPrecision(6)
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("update_by");
        });

        modelBuilder.Entity<InformationUser>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__Informat__66DCF95DD89F37B1");

            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("userName");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("phone_number");
            entity.Property(e => e.PlanExpired)
                .HasColumnType("datetime")
                .HasColumnName("plan_expired");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)")
                .HasFillFactor(100);

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });
        
        modelBuilder.Entity<SystemConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SystemCo__3213E83FC03737A9");

            entity.ToTable("SystemConfig");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("created_by");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.ScreenName)
                .HasMaxLength(100)
                .HasColumnName("screen_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("updated_by");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex").HasFillFactor(100);

            entity.HasIndex(e => e.RoleId, "IX_Users_RoleId").HasFillFactor(100);

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)")
                .HasFillFactor(100);

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasMany(d => d.Roles).WithMany(p => p.UsersNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_AspNetUserRoles_Roles_RoleId"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_AspNetUserRoles_Users_UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK_AspNetUserRoles");
                        j.ToTable("UserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId").HasFillFactor(100);
                    });
        });
        
        modelBuilder.Entity<VwEmailTemplateVerifyOtp>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_EmailTemplate_VerifyOTP");

            entity.Property(e => e.EmailBody).HasColumnName("email_body");
            entity.Property(e => e.EmailTitle).HasColumnName("email_title");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.ScreenName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("screen_name");
        });

        modelBuilder.Entity<VwEmailTemplateVerifyUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_EmailTemplate_VerifyUser");

            entity.Property(e => e.EmailBody).HasColumnName("email_body");
            entity.Property(e => e.EmailTitle).HasColumnName("email_title");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.ScreenName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("screen_name");
        });

        modelBuilder.Entity<VwRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Role");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<VwUserAuthentication>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_UserAuthentication");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<VwUserLogin>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_UserLogin");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.RoleName).HasMaxLength(256);
            entity.Property(e => e.UserId).HasMaxLength(450);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<VwUserVerify>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_UserVerify");

            entity.Property(e => e.Key).HasColumnName("key");
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
