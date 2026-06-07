using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Departments.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Departments.Commands.UpdateDepartment;
public record UpdateDepartmentCommand(Guid Id, string Code, string Name, string? Description, Guid? ParentDepartmentId, Guid? ManagerId, bool IsActive) : IRequest<Result<DepartmentDto>>;
public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Result<DepartmentDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public UpdateDepartmentCommandHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<DepartmentDto>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var dept = await _uow.Repository<Department>().GetByIdAsync(request.Id, cancellationToken);
        if (dept is null) return Result<DepartmentDto>.Failure($"Department '{request.Id}' not found.");
        dept.Code = request.Code; dept.Name = request.Name; dept.Description = request.Description;
        dept.ParentDepartmentId = request.ParentDepartmentId; dept.ManagerId = request.ManagerId; dept.IsActive = request.IsActive;
        dept.UpdatedAt = DateTime.UtcNow;
        _uow.Repository<Department>().Update(dept);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result<DepartmentDto>.Success(_mapper.Map<DepartmentDto>(dept));
    }
}
