using EmployeeManagement.Data.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Configurations.BaseConfigurations;

public static class BaseConfigurations
{
    public static EntityTypeBuilder<TEntity> AddBaseEntityConfiguration<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : BaseEntity<string>
    {
        builder.Property(b => b.IsDeleted).HasDefaultValue(false);
        builder.Property(b => b.Id).HasDefaultValueSql("gen_random_uuid()");
        return builder;
    }
    public static EntityTypeBuilder<TEntity> AddBaseAuditableConfiguration<TEntity>(this EntityTypeBuilder<TEntity> builder)
       where TEntity : BaseAuditable<string>
    {
        builder.AddBaseEntityConfiguration();
        builder.Property(b => b.CreatedBy).IsRequired();
        builder.Property(b => b.CreatedDate).HasDefaultValueSql("now() at time zone 'utc'");
        builder.Property(b => b.UpdatedBy).IsRequired(false);
        return builder;
    }
}
