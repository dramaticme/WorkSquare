using Microsoft.EntityFrameworkCore;
using worksquare.Model;

namespace worksquare.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // ── Core entities ────────────────────────────────────────────────
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyDetails> CompanyDetails { get; set; }
        public DbSet<CompanyUser> CompanyUsers { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDetail> EmployeeDetails { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }

        // ── Auth entities ─────────────────────────────────────────────────
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }

        // ── Permission entities ───────────────────────────────────────────
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
