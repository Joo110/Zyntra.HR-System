using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Departments.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Departments.Commands.CreateDepartment;
public record CreateDepartmentCommand(string Code, string Name, string? Description, Guid? ParentDepartmentId, Guid? ManagerId) : IRequest<Result<DepartmentDto>>;
public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<DepartmentDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public CreateDepartmentCommandHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<DepartmentDto>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var exists = await _uow.Repository<Department>().AnyAsync(d => d.Code == request.Code && !d.IsDeleted, cancellationToken);
        if (exists) return Result<DepartmentDto>.Failure($"Department code '{request.Code}' already exists.");
        var dept = new Department { Code = request.Code, Name = request.Name, Description = request.Description, ParentDepartmentId = request.ParentDepartmentId, ManagerId = request.ManagerId };
        await _uow.Repository<Department>().AddAsync(dept, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result<DepartmentDto>.Success(_mapper.Map<DepartmentDto>(dept));
    }
}
