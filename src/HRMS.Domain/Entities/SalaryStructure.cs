namespace HRMS.Domain.Entities;
public class SalaryStructure : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal BasicSalary { get; set; }
    public string Currency { get; set; } = "USD";
    public bool IsActive { get; set; } = true;
    public virtual ICollection<SalaryAllowance> Allowances { get; set; } = new List<SalaryAllowance>();
    public virtual ICollection<SalaryDeduction> Deductions { get; set; } = new List<SalaryDeduction>();
}
