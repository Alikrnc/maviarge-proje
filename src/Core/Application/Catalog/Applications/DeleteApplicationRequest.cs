using FSH.WebApi.Application.Catalog.Interviews;
using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Applications;

public class DeleteApplicationRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteApplicationRequest(Guid id) => Id = id;
}

public class DeleteApplicationRequestHandler : IRequestHandler<DeleteApplicationRequest, Guid>
{
    private readonly IRepositoryWithEvents<FSH.WebApi.Domain.Catalog.Application> _applicationRepo;
    private readonly IReadRepository<Interview> _interviewRepo;
    private readonly IStringLocalizer _t;

    public DeleteApplicationRequestHandler(IRepositoryWithEvents<FSH.WebApi.Domain.Catalog.Application> applicationRepo, IReadRepository<Interview> interviewRepo, IStringLocalizer<DeleteApplicationRequestHandler> localizer) =>
        (_applicationRepo, _interviewRepo, _t) = (applicationRepo, interviewRepo, localizer);

    public async Task<Guid> Handle(DeleteApplicationRequest request, CancellationToken cancellationToken)
    {
        if (await _interviewRepo.AnyAsync(new InterviewsByApplicationSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["Application cannot be deleted as it's being used."]);
        }

        var application = await _applicationRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = application ?? throw new NotFoundException(_t["Application {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        application.DomainEvents.Add(EntityDeletedEvent.WithEntity(application));

        await _applicationRepo.DeleteAsync(application, cancellationToken);

        return request.Id;
    }
}