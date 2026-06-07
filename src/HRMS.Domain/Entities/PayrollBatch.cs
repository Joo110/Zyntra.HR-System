using HRMS.Domain.Enums;
namespace HRMS.Domain.Entities;
public class PayrollBatch : BaseEntity
{
    public string BatchCode { get; set; } = string.Empty;
    public int Month { get; set; }
    public int Year { get; set; }
    public DateTime ProcessedDate { get; set; }
    public PayrollStatus Status { get; set; } = PayrollStatus.Draft;
    public decimal TotalGross { get; set; }
    public decimal TotalDeductions { get; set; }
    public decimal TotalNet { get; set; }
    public string? Notes { get; set; }
    public virtual ICollection<Payslip> Payslips { get; set; } = new List<Payslip>();
}
