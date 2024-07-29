using FSH.WebApi.Application.Identity.Users;
using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Applications;

public class CreateApplicationRequest : IRequest<Guid>
{
    public string? Description { get; set; }
    public Guid JobPostingId { get; set; }
}

public class CreateApplicationRequestHandler : IRequestHandler<CreateApplicationRequest, Guid>
{
    private readonly IRepository<FSH.WebApi.Domain.Catalog.Application> _repository;
    private readonly IRepository<FSH.WebApi.Domain.Catalog.JobPosting> _jobRepo;
    private readonly IFileStorageService _file;
    private readonly ICurrentUser _currentUser;

    public CreateApplicationRequestHandler(IRepository<FSH.WebApi.Domain.Catalog.Application> repository, IRepository<JobPosting> jobRepo, ICurrentUser currentUser, IFileStorageService file) =>
        (_repository, _jobRepo, _file, _currentUser) = (repository, jobRepo, file, currentUser);

    public async Task<Guid> Handle(CreateApplicationRequest request, CancellationToken cancellationToken)
    {
        // Fetch the job posting using the JobPostingId
        var jobPosting = await _jobRepo.GetByIdAsync(request.JobPostingId, cancellationToken) ?? throw new NotFoundException($"Job posting with ID {request.JobPostingId} not found.");

        string jobPostingName = jobPosting.Name;
        Guid userId = _currentUser.GetUserId();
        string firstName = _currentUser.GetUserFirstName();
        string lastName = _currentUser.GetUserLastName();
        string applicationName = $"Basvuru_{userId}";

        var application = new FSH.WebApi.Domain.Catalog.Application(applicationName, request.Description, request.JobPostingId, userId, firstName, lastName);

        // Add Domain Events to be raised after the commit
        application.DomainEvents.Add(EntityCreatedEvent.WithEntity(application));

        await _repository.AddAsync(application, cancellationToken);

        return application.Id;
    }
}