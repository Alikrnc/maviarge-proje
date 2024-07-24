using FSH.WebApi.Application.Catalog.Applications;

namespace FSH.WebApi.Application.Catalog.JobPostings;

public class DeleteJobPostingRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteJobPostingRequest(Guid id) => Id = id;
}

public class DeleteJobPostingRequestHandler : IRequestHandler<DeleteJobPostingRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<JobPosting> _jobpostingRepo;
    private readonly IReadRepository<FSH.WebApi.Domain.Catalog.Application> _applicationRepo;
    private readonly IStringLocalizer _t;

    public DeleteJobPostingRequestHandler(IRepositoryWithEvents<JobPosting> jobpostingRepo, IReadRepository<FSH.WebApi.Domain.Catalog.Application> applicationRepo, IStringLocalizer<DeleteJobPostingRequestHandler> localizer) =>
        (_jobpostingRepo, _applicationRepo, _t) = (jobpostingRepo, applicationRepo, localizer);

    public async Task<Guid> Handle(DeleteJobPostingRequest request, CancellationToken cancellationToken)
    {
        if (await _applicationRepo.AnyAsync(new ApplicationsByJobPostingSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["Job Posting cannot be deleted as it's being used."]);
        }

        var jobposting = await _jobpostingRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = jobposting ?? throw new NotFoundException(_t["Job Posting {0} Not Found."]);

        await _jobpostingRepo.DeleteAsync(jobposting, cancellationToken);

        return request.Id;
    }
}