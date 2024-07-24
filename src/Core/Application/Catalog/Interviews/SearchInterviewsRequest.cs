namespace FSH.WebApi.Application.Catalog.Interviews;

public class SearchInterviewsRequest : PaginationFilter, IRequest<PaginationResponse<InterviewDto>>
{
    public Guid? ApplicationId { get; set; }
}

public class SearchInterviewsRequestHandler : IRequestHandler<SearchInterviewsRequest, PaginationResponse<InterviewDto>>
{
    private readonly IReadRepository<Interview> _repository;

    public SearchInterviewsRequestHandler(IReadRepository<Interview> repository) => _repository = repository;

    public async Task<PaginationResponse<InterviewDto>> Handle(SearchInterviewsRequest request, CancellationToken cancellationToken)
    {
        var spec = new InterviewsBySearchRequestWithApplicationsSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}