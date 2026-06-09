using AutoMapper; 
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Localization;
using HRMS.Application.Modules.Branches.DTOs; 
using HRMS.Domain.Entities; 
using HRMS.Shared.Models;
using MediatR;
using Microsoft.Extensions.Localization;
namespace HRMS.Application.Modules.Branches.Commands.UpdateBranch;
public record UpdateBranchCommand(Guid Id, string Code, string Name, string? Address, string? City, string? Country, string? Phone, string? Email, bool IsActive) : IRequest<Result<BranchDto>>;
public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, Result<BranchDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    private ILocalizationService _localizer;

    public UpdateBranchCommandHandler(IUnitOfWork uow, IMapper mapper, ILocalizationService localizer)
    {
        _uow = uow; _mapper = mapper;
        _localizer = localizer;
    }
    public async Task<Result<BranchDto>> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        var b = await _uow.Repository<Branch>().GetByIdAsync(request.Id, cancellationToken);
        if (b is null) return Result<BranchDto>.Failure(_localizer["Branchnotfound"]);
        b.Code = request.Code; b.Name = request.Name; b.Address = request.Address; b.City = request.City; b.Country = request.Country; b.Phone = request.Phone; b.Email = request.Email; b.IsActive = request.IsActive; b.UpdatedAt = DateTime.UtcNow;
        _uow.Repository<Branch>().Update(b);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result<BranchDto>.Success(_mapper.Map<BranchDto>(b));
    }
}
