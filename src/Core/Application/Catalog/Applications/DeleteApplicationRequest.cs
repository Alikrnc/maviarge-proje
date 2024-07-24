using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Applications;

public class DeleteApplicationRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteApplicationRequest(Guid id) => Id = id;
}

public class DeleteApplicationRequestHandler : IRequestHandler<DeleteApplicationRequest, Guid>
{
    private readonly IRepository<FSH.WebApi.Domain.Catalog.Application> _repository;
    private readonly IStringLocalizer _t;

    public DeleteApplicationRequestHandler(IRepository<FSH.WebApi.Domain.Catalog.Application> repository, IStringLocalizer<DeleteApplicationRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteApplicationRequest request, CancellationToken cancellationToken)
    {
        var application = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = application ?? throw new NotFoundException(_t["Application {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        application.DomainEvents.Add(EntityDeletedEvent.WithEntity(application));

        await _repository.DeleteAsync(application, cancellationToken);

        return request.Id;
    }
}