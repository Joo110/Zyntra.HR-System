using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Branches.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Branches.Queries.GetBranchById;
public record GetBranchByIdQuery(Guid Id) : IRequest<Result<BranchDto>>;
public class GetBranchByIdQueryHandler : IRequestHandler<GetBranchByIdQuery, Result<BranchDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetBranchByIdQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<BranchDto>> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
    {
        var b = await _uow.Repository<Branch>().GetByIdAsync(request.Id, cancellationToken);
        if (b is null || b.IsDeleted) return Result<BranchDto>.Failure("Branch not found.");
        return Result<BranchDto>.Success(_mapper.Map<BranchDto>(b));
    }
}
