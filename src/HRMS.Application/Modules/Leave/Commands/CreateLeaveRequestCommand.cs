using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Leave.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Leave.Commands.CreateLeaveRequest;
public record CreateLeaveRequestCommand(Guid EmployeeId, Guid LeaveTypeId, DateTime StartDate, DateTime EndDate, string Reason) : IRequest<Result<LeaveRequestDto>>;
public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Result<LeaveRequestDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public CreateLeaveRequestCommandHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<LeaveRequestDto>> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var days = (request.EndDate - request.StartDate).TotalDays + 1;
        var leave = new LeaveRequest { EmployeeId = request.EmployeeId, LeaveTypeId = request.LeaveTypeId, StartDate = request.StartDate, EndDate = request.EndDate, NumberOfDays = days, Reason = request.Reason };
        await _uow.Repository<LeaveRequest>().AddAsync(leave, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result<LeaveRequestDto>.Success(_mapper.Map<LeaveRequestDto>(leave));
    }
}
