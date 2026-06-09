using AutoMapper; 
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Localization;
using HRMS.Application.Modules.Branches.DTOs; 
using HRMS.Domain.Entities; 
using HRMS.Shared.Models;
using MediatR;
using Microsoft.Extensions.Localization;
namespace HRMS.Application.Modules.Branches.Queries.GetBranchById;
public record GetBranchByIdQuery(Guid Id) : IRequest<Result<BranchDto>>;
public class GetBranchByIdQueryHandler : IRequestHandler<GetBranchByIdQuery, Result<BranchDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    private IStringLocalizer<SharedResource> _localizer;
    public GetBranchByIdQueryHandler(IUnitOfWork uow, IMapper mapper, IStringLocalizer<SharedResource> localizer)
    {
        _uow = uow; _mapper = mapper;
        _localizer = localizer;
    }
    public async Task<Result<BranchDto>> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
    {
        var b = await _uow.Repository<Branch>().GetByIdAsync(request.Id, cancellationToken);
        if (b is null || b.IsDeleted) return Result<BranchDto>.Failure(_localizer["Branchnotfound"]);
        return Result<BranchDto>.Success(_mapper.Map<BranchDto>(b));
    }
}
