namespace FSH.WebApi.Application.Catalog.Applications;

public class SearchApplicationsRequest : PaginationFilter, IRequest<PaginationResponse<ApplicationDto>>
{
    public Guid? JobPostingId { get; set; }
    public Guid? CandidateInfoId { get; set; }
}

public class SearchApplicationsRequestHandler : IRequestHandler<SearchApplicationsRequest, PaginationResponse<ApplicationDto>>
{
    private readonly IReadRepository<FSH.WebApi.Domain.Catalog.Application> _repository;

    public SearchApplicationsRequestHandler(IReadRepository<FSH.WebApi.Domain.Catalog.Application> repository) => _repository = repository;

    public async Task<PaginationResponse<ApplicationDto>> Handle(SearchApplicationsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ApplicationsBySearchRequestWithJobPostingsSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}