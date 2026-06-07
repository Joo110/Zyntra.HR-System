using AutoMapper; using HRMS.Application.Modules.Payroll.DTOs; using HRMS.Domain.Entities;
namespace HRMS.Application.Modules.Payroll.Mappings;
public class PayrollMappingProfile : Profile
{
    public PayrollMappingProfile()
    {
        CreateMap<PayrollBatch, PayrollBatchDto>();
        CreateMap<Payslip, PayslipDto>().ForMember(d => d.EmployeeName, o => o.MapFrom(s => s.Employee != null ? $"{s.Employee.FirstName} {s.Employee.LastName}" : null));
    }
}
