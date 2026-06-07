using HRMS.Domain.Enums;
namespace HRMS.Domain.Entities;
public class Payslip : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Guid PayrollBatchId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal TotalAllowances { get; set; }
    public decimal TotalDeductions { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal NetSalary { get; set; }
    public PayrollStatus Status { get; set; } = PayrollStatus.Draft;
    public string? Notes { get; set; }
    public virtual Employee? Employee { get; set; }
    public virtual PayrollBatch? PayrollBatch { get; set; }
}
