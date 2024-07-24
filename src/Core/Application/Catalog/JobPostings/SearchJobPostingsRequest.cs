namespace FSH.WebApi.Application.Catalog.JobPostings;

public class SearchJobPostingsRequest : PaginationFilter, IRequest<PaginationResponse<JobPostingDto>>
{
}

public class JobPostingsBySearchRequestSpec : EntitiesByPaginationFilterSpec<JobPosting, JobPostingDto>
{
    public JobPostingsBySearchRequestSpec(SearchJobPostingsRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchJobPostingsRequestHandler : IRequestHandler<SearchJobPostingsRequest, PaginationResponse<JobPostingDto>>
{
    private readonly IReadRepository<JobPosting> _repository;

    public SearchJobPostingsRequestHandler(IReadRepository<JobPosting> repository) => _repository = repository;

    public async Task<PaginationResponse<JobPostingDto>> Handle(SearchJobPostingsRequest request, CancellationToken cancellationToken)
    {
        var spec = new JobPostingsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}