using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Interviews;

public class CreateInterviewRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid ApplicationId { get; set; }
}

public class CreateInterviewRequestHandler : IRequestHandler<CreateInterviewRequest, Guid>
{
    private readonly IRepository<Interview> _repository;
    private readonly IFileStorageService _file;

    public CreateInterviewRequestHandler(IRepository<Interview> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateInterviewRequest request, CancellationToken cancellationToken)
    {
        var interview = new Interview(request.Name, request.Description, request.ApplicationId);

        // Add Domain Events to be raised after the commit
        interview.DomainEvents.Add(EntityCreatedEvent.WithEntity(interview));

        await _repository.AddAsync(interview, cancellationToken);

        return interview.Id;
    }
}