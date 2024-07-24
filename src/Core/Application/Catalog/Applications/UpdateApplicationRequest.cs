using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Applications;

public class UpdateApplicationRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid JobPostingId { get; set; }
    public Guid CandidateInfoId { get; set; }
}

public class UpdateApplicationRequestHandler : IRequestHandler<UpdateApplicationRequest, Guid>
{
    private readonly IRepository<FSH.WebApi.Domain.Catalog.Application> _repository;
    private readonly IStringLocalizer _t;
    private readonly IFileStorageService _file;

    public UpdateApplicationRequestHandler(IRepository<FSH.WebApi.Domain.Catalog.Application> repository, IStringLocalizer<UpdateApplicationRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _t, _file) = (repository, localizer, file);

    public async Task<Guid> Handle(UpdateApplicationRequest request, CancellationToken cancellationToken)
    {
        var application = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = application ?? throw new NotFoundException(_t["Application {0} Not Found.", request.Id]);

        var updatedApplication = application.Update(request.Name, request.Description, request.JobPostingId, request.CandidateInfoId);

        // Add Domain Events to be raised after the commit
        application.DomainEvents.Add(EntityUpdatedEvent.WithEntity(application));

        await _repository.UpdateAsync(updatedApplication, cancellationToken);

        return request.Id;
    }
}