using EmployeeManagement.Data.Base;
using EmployeeManagement.Data.Entities;
using EmployeeManagement.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace EmployeeManagement.DataAccess.Context;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<Department>().HasData(new Department
        {
            Id = Guid.NewGuid().ToString(),
            Name = "D 1",
        });

        base.OnModelCreating(builder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<EntityEntry<BaseEntity>> entities = ChangeTracker.Entries<BaseEntity>();
        foreach (EntityEntry<BaseEntity> entity in entities)
        {
            if (entity.State == EntityState.Deleted)
            {
                entity.Entity.IsDeleted = true;
            }
        }
        IEnumerable<EntityEntry<BaseAuditable>> entitiesAuditable = ChangeTracker.Entries<BaseAuditable>();
        foreach (EntityEntry<BaseAuditable> entity in entitiesAuditable)
        {
            if (entity.State == EntityState.Deleted)
            {
                entity.Entity.IsDeleted = true;
            }
            else if (entity.State == EntityState.Modified)
            {
                entity.Entity.UpdatedDate = DateTime.UtcNow;
                entity.Entity.UpdatedBy = "user 1"; //Todo: User
            }
            else if (entity.State == EntityState.Added)
            {
                entity.Entity.CreatedBy = "user 1"; //Todo: User
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
