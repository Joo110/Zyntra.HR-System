using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Notifications.DTOs; using HRMS.Domain.Entities; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Notifications.Queries.GetAllNotifications;
public record GetAllNotificationsQuery(string? RecipientId, int PageNumber = 1, int PageSize = 20) : IRequest<Result<PagedResult<NotificationDto>>>;
public class GetAllNotificationsQueryHandler : IRequestHandler<GetAllNotificationsQuery, Result<PagedResult<NotificationDto>>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public GetAllNotificationsQueryHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<PagedResult<NotificationDto>>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
    {
        var query = _uow.Repository<Notification>().GetQueryable().Where(n => !n.IsDeleted);
        if (!string.IsNullOrEmpty(request.RecipientId)) query = query.Where(n => n.RecipientId == request.RecipientId);
        var total = query.Count();
        var items = query.OrderByDescending(n => n.CreatedAt).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
        return Result<PagedResult<NotificationDto>>.Success(PagedResult<NotificationDto>.Create(_mapper.Map<IEnumerable<NotificationDto>>(items), total, request.PageNumber, request.PageSize));
    }
}
