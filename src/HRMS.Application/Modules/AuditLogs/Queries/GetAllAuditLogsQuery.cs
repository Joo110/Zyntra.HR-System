using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.AuditLogs.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.AuditLogs.Queries.GetAllAuditLogs;
public record GetAllAuditLogsQuery(string? UserId, string? EntityName, int PageNumber = 1, int PageSize = 20) : IRequest<Result<PagedResult<AuditLogDto>>>;
public class GetAllAuditLogsQueryHandler : IRequestHandler<GetAllAuditLogsQuery, Result<PagedResult<AuditLogDto>>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetAllAuditLogsQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<PagedResult<AuditLogDto>>> Handle(GetAllAuditLogsQuery request, CancellationToken cancellationToken)
    {
        var query = _uow.Repository<AuditLog>().GetQueryable();
        if (!string.IsNullOrEmpty(request.UserId)) query = query.Where(a => a.UserId == request.UserId);
        if (!string.IsNullOrEmpty(request.EntityName)) query = query.Where(a => a.EntityName == request.EntityName);
        var total = query.Count();
        var items = query.OrderByDescending(a => a.Timestamp).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
        return Result<PagedResult<AuditLogDto>>.Success(PagedResult<AuditLogDto>.Create(_mapper.Map<IEnumerable<AuditLogDto>>(items), total, request.PageNumber, request.PageSize));
    }
}
