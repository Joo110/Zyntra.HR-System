using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Branches.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Branches.Queries.GetAllBranches;
public record GetAllBranchesQuery(int PageNumber = 1, int PageSize = 20) : IRequest<Result<PagedResult<BranchDto>>>;
public class GetAllBranchesQueryHandler : IRequestHandler<GetAllBranchesQuery, Result<PagedResult<BranchDto>>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetAllBranchesQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<PagedResult<BranchDto>>> Handle(GetAllBranchesQuery request, CancellationToken cancellationToken)
    {
        var query = _uow.Repository<Branch>().GetQueryable().Where(b => !b.IsDeleted);
        var total = query.Count();
        var items = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
        return Result<PagedResult<BranchDto>>.Success(PagedResult<BranchDto>.Create(_mapper.Map<IEnumerable<BranchDto>>(items), total, request.PageNumber, request.PageSize));
    }
}
