using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Positions.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Positions.Commands.CreatePosition;
public record CreatePositionCommand(string Code, string Title, string? Description, Guid? DepartmentId, decimal? MinSalary, decimal? MaxSalary) : IRequest<Result<PositionDto>>;
public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, Result<PositionDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public CreatePositionCommandHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<PositionDto>> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        var pos = new Position { Code = request.Code, Title = request.Title, Description = request.Description, DepartmentId = request.DepartmentId, MinSalary = request.MinSalary, MaxSalary = request.MaxSalary };
        await _uow.Repository<Position>().AddAsync(pos, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result<PositionDto>.Success(_mapper.Map<PositionDto>(pos));
    }
}
