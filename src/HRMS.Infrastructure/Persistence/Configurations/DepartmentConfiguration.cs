using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HRMS.Domain.Entities;
namespace HRMS.Infrastructure.Persistence.Configurations;
public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Code).IsRequired().HasMaxLength(50);
        builder.Property(d => d.Name).IsRequired().HasMaxLength(200);
        builder.HasIndex(d => d.Code).IsUnique();
        builder.HasOne(d => d.ParentDepartment).WithMany(p => p.SubDepartments).HasForeignKey(d => d.ParentDepartmentId).OnDelete(DeleteBehavior.Restrict);
        builder.ToTable("Departments");
    }
}
