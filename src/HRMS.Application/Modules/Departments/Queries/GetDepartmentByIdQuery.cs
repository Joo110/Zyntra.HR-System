using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Departments.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Departments.Queries.GetDepartmentById;
public record GetDepartmentByIdQuery(Guid Id) : IRequest<Result<DepartmentDto>>;
public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, Result<DepartmentDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    private ILocalizationService _localizer;
    public GetDepartmentByIdQueryHandler(IUnitOfWork uow, IMapper mapper, ILocalizationService localizer)
    {
        _uow = uow; _mapper = mapper;
        _localizer = localizer;
    }
    public async Task<Result<DepartmentDto>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var dept = await _uow.Repository<Department>().GetByIdAsync(request.Id, cancellationToken);
        if (dept is null || dept.IsDeleted) return Result<DepartmentDto>.Failure(_localizer["Departmentnotfound", request.Id]);
        return Result<DepartmentDto>.Success(_mapper.Map<DepartmentDto>(dept));
    }
}
