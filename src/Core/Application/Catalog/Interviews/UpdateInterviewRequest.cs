using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Interviews;

public class UpdateInterviewRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid ApplicationId { get; set; }
}

public class UpdateInterviewRequestHandler : IRequestHandler<UpdateInterviewRequest, Guid>
{
    private readonly IRepository<Interview> _repository;
    private readonly IStringLocalizer _t;
    private readonly IFileStorageService _file;

    public UpdateInterviewRequestHandler(IRepository<Interview> repository, IStringLocalizer<UpdateInterviewRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _t, _file) = (repository, localizer, file);

    public async Task<Guid> Handle(UpdateInterviewRequest request, CancellationToken cancellationToken)
    {
        var interview = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = interview ?? throw new NotFoundException(_t["Interview {0} Not Found.", request.Id]);

        var updatedInterview = interview.Update(request.Name, request.Description, request.ApplicationId);

        // Add Domain Events to be raised after the commit
        interview.DomainEvents.Add(EntityUpdatedEvent.WithEntity(interview));

        await _repository.UpdateAsync(updatedInterview, cancellationToken);

        return request.Id;
    }
}