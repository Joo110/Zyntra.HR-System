namespace HRMS.Domain.Entities;
public class SalaryDeduction : BaseEntity
{
    public Guid SalaryStructureId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public bool IsPercentage { get; set; } = false;
    public bool IsCompulsory { get; set; } = false;
    public virtual SalaryStructure? SalaryStructure { get; set; }
}
