using MediatR; using AutoMapper; using HRMS.Application.Common.Interfaces; using HRMS.Application.Modules.Performance.DTOs; using HRMS.Domain.Entities; using HRMS.Domain.Enums; using HRMS.Shared.Models;
namespace HRMS.Application.Modules.Performance.Commands.CreatePerformanceReview;
public record CreatePerformanceReviewCommand(Guid EmployeeId, int ReviewYear, int? ReviewQuarter, string ReviewPeriod, PerformanceRating? SelfRating, string? SelfComments) : IRequest<Result<PerformanceReviewDto>>;
public class CreatePerformanceReviewCommandHandler : IRequestHandler<CreatePerformanceReviewCommand, Result<PerformanceReviewDto>>
{
    private readonly IUnitOfWork _uow; private readonly IMapper _mapper;
    public CreatePerformanceReviewCommandHandler(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }
    public async Task<Result<PerformanceReviewDto>> Handle(CreatePerformanceReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new PerformanceReview { EmployeeId = request.EmployeeId, ReviewYear = request.ReviewYear, ReviewQuarter = request.ReviewQuarter, ReviewPeriod = request.ReviewPeriod, SelfRating = request.SelfRating, SelfComments = request.SelfComments };
        await _uow.Repository<PerformanceReview>().AddAsync(review, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result<PerformanceReviewDto>.Success(_mapper.Map<PerformanceReviewDto>(review));
    }
}
