using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HRMS.Domain.Entities;
namespace HRMS.Infrastructure.Persistence.Configurations;
public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.EmployeeCode).IsRequired().HasMaxLength(50);
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(320);
        builder.HasIndex(e => e.Email).IsUnique();
        builder.HasIndex(e => e.EmployeeCode).IsUnique();
        builder.Property(e => e.RowVersion).IsRowVersion();
        builder.HasOne(e => e.Department).WithMany(d => d.Employees).HasForeignKey(e => e.DepartmentId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(e => e.Position).WithMany(p => p.Employees).HasForeignKey(e => e.PositionId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(e => e.Branch).WithMany(b => b.Employees).HasForeignKey(e => e.BranchId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(e => e.Manager).WithMany(m => m.Subordinates).HasForeignKey(e => e.ManagerId).OnDelete(DeleteBehavior.Restrict);
        builder.ToTable("Employees");
    }
}
