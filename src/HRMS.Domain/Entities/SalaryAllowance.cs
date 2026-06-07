namespace HRMS.Domain.Entities;
public class SalaryAllowance : BaseEntity
{
    public Guid SalaryStructureId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public bool IsPercentage { get; set; } = false;
    public bool IsTaxable { get; set; } = true;
    public virtual SalaryStructure? SalaryStructure { get; set; }
}
