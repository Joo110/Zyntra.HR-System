using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Positions.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Positions.Commands.UpdatePosition;
public record UpdatePositionCommand(Guid Id, string Code, string Title, string? Description, Guid? DepartmentId, decimal? MinSalary, decimal? MaxSalary, bool IsActive) : IRequest<Result<PositionDto>>;
public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, Result<PositionDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public UpdatePositionCommandHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<PositionDto>> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        var pos = await _uow.Repository<Position>().GetByIdAsync(request.Id, cancellationToken);
        if (pos is null) return Result<PositionDto>.Failure("Position not found.");
        pos.Code = request.Code; pos.Title = request.Title; pos.Description = request.Description; pos.DepartmentId = request.DepartmentId; pos.MinSalary = request.MinSalary; pos.MaxSalary = request.MaxSalary; pos.IsActive = request.IsActive; pos.UpdatedAt = DateTime.UtcNow;
        _uow.Repository<Position>().Update(pos);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result<PositionDto>.Success(_mapper.Map<PositionDto>(pos));
    }
}
