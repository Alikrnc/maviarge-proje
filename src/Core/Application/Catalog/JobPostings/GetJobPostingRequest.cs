namespace FSH.WebApi.Application.Catalog.JobPostings;

public class GetJobPostingRequest : IRequest<JobPostingDto>
{
    public Guid Id { get; set; }

    public GetJobPostingRequest(Guid id) => Id = id;
}

public class JobPostingByIdSpec : Specification<JobPosting, JobPostingDto>, ISingleResultSpecification
{
    public JobPostingByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetJobPostingRequestHandler : IRequestHandler<GetJobPostingRequest, JobPostingDto>
{
    private readonly IRepository<JobPosting> _repository;
    private readonly IStringLocalizer _t;

    public GetJobPostingRequestHandler(IRepository<JobPosting> repository, IStringLocalizer<GetJobPostingRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<JobPostingDto> Handle(GetJobPostingRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<JobPosting, JobPostingDto>)new JobPostingByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Job Posting {0} Not Found.", request.Id]);
}