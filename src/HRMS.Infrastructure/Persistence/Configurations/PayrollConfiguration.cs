using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HRMS.Domain.Entities;
namespace HRMS.Infrastructure.Persistence.Configurations;
public class PayrollBatchConfiguration : IEntityTypeConfiguration<PayrollBatch>
{
    public void Configure(EntityTypeBuilder<PayrollBatch> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.BatchCode).IsRequired().HasMaxLength(50);
        builder.Property(p => p.TotalGross).HasPrecision(18, 2);
        builder.Property(p => p.TotalDeductions).HasPrecision(18, 2);
        builder.Property(p => p.TotalNet).HasPrecision(18, 2);
        builder.ToTable("PayrollBatches");
    }
}
public class PayslipConfiguration : IEntityTypeConfiguration<Payslip>
{
    public void Configure(EntityTypeBuilder<Payslip> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.BasicSalary).HasPrecision(18, 2);
        builder.Property(p => p.TotalAllowances).HasPrecision(18, 2);
        builder.Property(p => p.TotalDeductions).HasPrecision(18, 2);
        builder.Property(p => p.TaxAmount).HasPrecision(18, 2);
        builder.Property(p => p.NetSalary).HasPrecision(18, 2);
        builder.HasOne(p => p.Employee).WithMany().HasForeignKey(p => p.EmployeeId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(p => p.PayrollBatch).WithMany(b => b.Payslips).HasForeignKey(p => p.PayrollBatchId).OnDelete(DeleteBehavior.Cascade);
        builder.ToTable("Payslips");
    }
}
