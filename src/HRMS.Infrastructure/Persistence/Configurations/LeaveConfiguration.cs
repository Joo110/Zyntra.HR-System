using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HRMS.Domain.Entities;
namespace HRMS.Infrastructure.Persistence.Configurations;
public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
{
    public void Configure(EntityTypeBuilder<LeaveRequest> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Reason).IsRequired().HasMaxLength(1000);
        builder.HasOne(l => l.Employee).WithMany(e => e.LeaveRequests).HasForeignKey(l => l.EmployeeId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(l => l.LeaveType).WithMany(t => t.LeaveRequests).HasForeignKey(l => l.LeaveTypeId).OnDelete(DeleteBehavior.Restrict);
        builder.ToTable("LeaveRequests");
    }
}

public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Code).IsRequired().HasMaxLength(50);
        builder.Property(l => l.Name).IsRequired().HasMaxLength(200);
        builder.HasIndex(l => l.Code).IsUnique();
        builder.ToTable("LeaveTypes");
    }
}
