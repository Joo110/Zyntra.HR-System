using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Notifications.DTOs; using HRMS.Domain.Entities; using HRMS.Domain.Enums; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Notifications.Commands.SendNotification;
public record SendNotificationCommand(string RecipientId, string Title, string Message, NotificationType Type) : IRequest<Result<NotificationDto>>;
public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, Result<NotificationDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper; private readonly INotificationService _notifService;
    public SendNotificationCommandHandler(IUnitOfWork uow, IMapper mapper, INotificationService notifService) { _uow = uow; _mapper = mapper; _notifService = notifService; }
    public async Task<Result<NotificationDto>> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        var notif = new Notification { Title = request.Title, Message = request.Message, Type = request.Type, RecipientId = request.RecipientId };
        await _uow.Repository<Notification>().AddAsync(notif, cancellationToken);
        await _notifService.SendAsync(request.RecipientId, request.Title, request.Message, request.Type, cancellationToken);
        notif.IsSent = true; notif.SentAt = DateTime.UtcNow;
        await _uow.SaveChangesAsync(cancellationToken);
        return Result<NotificationDto>.Success(_mapper.Map<NotificationDto>(notif));
    }
}
