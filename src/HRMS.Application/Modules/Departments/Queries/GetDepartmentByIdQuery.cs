using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Departments.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Departments.Queries.GetDepartmentById;
public record GetDepartmentByIdQuery(Guid Id) : IRequest<Result<DepartmentDto>>;
public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, Result<DepartmentDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetDepartmentByIdQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<DepartmentDto>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var dept = await _uow.Repository<Department>().GetByIdAsync(request.Id, cancellationToken);
        if (dept is null || dept.IsDeleted) return Result<DepartmentDto>.Failure($"Department '{request.Id}' not found.");
        return Result<DepartmentDto>.Success(_mapper.Map<DepartmentDto>(dept));
    }
}
