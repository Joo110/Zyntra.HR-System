using AutoMapper; 
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Localization;
using HRMS.Application.Modules.Departments.DTOs; 
using HRMS.Domain.Entities; 
using HRMS.Shared.Models;
using MediatR;
using Microsoft.Extensions.Localization;
namespace HRMS.Application.Modules.Departments.Commands.CreateDepartment;
public record CreateDepartmentCommand(string Code, string Name, string? Description, Guid? ParentDepartmentId, Guid? ManagerId) : IRequest<Result<DepartmentDto>>;
public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<DepartmentDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    private ILocalizationService _localizer;
    public CreateDepartmentCommandHandler(IUnitOfWork uow, IMapper mapper, ILocalizationService localizer)
    {
        _uow = uow; _mapper = mapper;
        _localizer = localizer;
    }
    public async Task<Result<DepartmentDto>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var exists = await _uow.Repository<Department>().AnyAsync(d => d.Code == request.Code && !d.IsDeleted, cancellationToken);
        if (exists)
        {
            return Result<DepartmentDto>.Failure(
                _localizer["DepartmentCodeExists", request.Code]);
        }
        var dept = new Department { Code = request.Code, Name = request.Name, Description = request.Description, ParentDepartmentId = request.ParentDepartmentId, ManagerId = request.ManagerId };
        await _uow.Repository<Department>().AddAsync(dept, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result<DepartmentDto>.Success(_mapper.Map<DepartmentDto>(dept));
    }
}
