

using Demo.DLL.Models.DepartmentModel;

namespace Demo.DLL.Data.configurations
{
  public class DepartmentConfigurations :BaseEntityConfigurations<Department> ,IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name).HasColumnType("varchar(20)");
            builder.Property(D => D.code).HasColumnType("varchar(20)");
           base.Configure(builder);
            
        }
    }
}
