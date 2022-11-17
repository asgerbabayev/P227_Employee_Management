using EmployeeManagement.Configurations.BaseConfigurations;
using EmployeeManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.DataAccess.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.AddBaseAuditableConfiguration();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(64);
        builder.Property(e => e.Surname).IsRequired().HasMaxLength(128);

        builder
            .HasOne(e => e.Department)
           .WithMany(e => e.Employees)
           .HasForeignKey(e => e.DepartmentId);
    }
}
