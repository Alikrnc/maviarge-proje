using FSH.WebApi.Domain.Common.Events;
using Microsoft.AspNetCore.Http;

namespace FSH.WebApi.Application.Catalog.Applications;

public class UploadCVRequest : IRequest<bool>
{
    public string Id { get; set; } = string.Empty;
    public IFormFile CV { get; set; } = default!;
    public string? CVPath { get; set; } = string.Empty;
}

public class UploadCVRequestHandler : IRequestHandler<UploadCVRequest, bool>
{
    private readonly IRepository<FSH.WebApi.Domain.Catalog.Application> _repository;
    private readonly IStringLocalizer _t;
    private readonly IFileStorageService _file;

    public UploadCVRequestHandler(IRepository<FSH.WebApi.Domain.Catalog.Application> repository, IStringLocalizer<UploadCVRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _t, _file) = (repository, localizer, file);

    public async Task<bool> Handle(UploadCVRequest request, CancellationToken cancellationToken)
    {
        var application = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = application ?? throw new NotFoundException(_t["Application {0} Not Found.", request.Id]);

        var updatedApplication = application.UploadCV(request.CVPath);

        // Add Domain Events to be raised after the commit
        application.DomainEvents.Add(EntityUpdatedEvent.WithEntity(application));

        await _repository.UpdateAsync(updatedApplication, cancellationToken);

        return true;
    }
}