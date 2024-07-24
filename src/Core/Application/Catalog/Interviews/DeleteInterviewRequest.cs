using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Interviews;

public class DeleteInterviewRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteInterviewRequest(Guid id) => Id = id;
}

public class DeleteInterviewRequestHandler : IRequestHandler<DeleteInterviewRequest, Guid>
{
    private readonly IRepository<Interview> _repository;
    private readonly IStringLocalizer _t;

    public DeleteInterviewRequestHandler(IRepository<Interview> repository, IStringLocalizer<DeleteInterviewRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteInterviewRequest request, CancellationToken cancellationToken)
    {
        var interview = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = interview ?? throw new NotFoundException(_t["Interview {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        interview.DomainEvents.Add(EntityDeletedEvent.WithEntity(interview));

        await _repository.DeleteAsync(interview, cancellationToken);

        return request.Id;
    }
}