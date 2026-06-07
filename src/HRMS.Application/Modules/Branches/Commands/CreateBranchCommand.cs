using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Branches.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Branches.Commands.CreateBranch;
public record CreateBranchCommand(string Code, string Name, string? Address, string? City, string? Country, string? Phone, string? Email) : IRequest<Result<BranchDto>>;
public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, Result<BranchDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public CreateBranchCommandHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<BranchDto>> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = new Branch { Code = request.Code, Name = request.Name, Address = request.Address, City = request.City, Country = request.Country, Phone = request.Phone, Email = request.Email };
        await _uow.Repository<Branch>().AddAsync(branch, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result<BranchDto>.Success(_mapper.Map<BranchDto>(branch));
    }
}
