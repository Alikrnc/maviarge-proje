using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Applications;

public class CreateApplicationRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid JobPostingId { get; set; }
    public Guid CandidateInfoId { get; set; }
}

public class CreateApplicationRequestHandler : IRequestHandler<CreateApplicationRequest, Guid>
{
    private readonly IRepository<FSH.WebApi.Domain.Catalog.Application> _repository;
    private readonly IFileStorageService _file;

    public CreateApplicationRequestHandler(IRepository<FSH.WebApi.Domain.Catalog.Application> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateApplicationRequest request, CancellationToken cancellationToken)
    {
        var application = new FSH.WebApi.Domain.Catalog.Application(request.Name, request.Description, request.JobPostingId, request.CandidateInfoId);

        // Add Domain Events to be raised after the commit
        application.DomainEvents.Add(EntityCreatedEvent.WithEntity(application));

        await _repository.AddAsync(application, cancellationToken);

        return application.Id;
    }
}