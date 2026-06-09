using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Leave.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Leave.Queries.GetLeaveById;
public record GetLeaveByIdQuery(Guid Id) : IRequest<Result<LeaveRequestDto>>;
public class GetLeaveByIdQueryHandler : IRequestHandler<GetLeaveByIdQuery, Result<LeaveRequestDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    private ILocalizationService _localizer;
    public GetLeaveByIdQueryHandler(IUnitOfWork uow, IMapper mapper, ILocalizationService localizer)
    {
        _uow = uow; _mapper = mapper;
        _localizer = localizer;
    }
    public async Task<Result<LeaveRequestDto>> Handle(GetLeaveByIdQuery request, CancellationToken cancellationToken)
    {
        var leave = await _uow.Repository<LeaveRequest>().GetByIdAsync(request.Id, cancellationToken);
        if (leave is null || leave.IsDeleted) return Result<LeaveRequestDto>.Failure(_localizer["Leaverequestnotfound"]);
        return Result<LeaveRequestDto>.Success(_mapper.Map<LeaveRequestDto>(leave));
    }
}
