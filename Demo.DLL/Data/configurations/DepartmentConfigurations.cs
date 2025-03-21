

namespace Demo.DLL.Data.configurations
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name).HasColumnType("varchar(20)");
            builder.Property(D => D.code).HasColumnType("varchar(20)");
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(D => D.LastModifedOn).HasComputedColumnSql("GETDATE()");
        }
    }
}
