namespace FSH.WebApi.Application.Catalog.CandidateInfos;

public class SearchCandidateInfosRequest : PaginationFilter, IRequest<PaginationResponse<CandidateInfoDto>>
{
}

public class CandidateInfosBySearchRequestSpec : EntitiesByPaginationFilterSpec<CandidateInfo, CandidateInfoDto>
{
    public CandidateInfosBySearchRequestSpec(SearchCandidateInfosRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchCandidateInfosRequestHandler : IRequestHandler<SearchCandidateInfosRequest, PaginationResponse<CandidateInfoDto>>
{
    private readonly IReadRepository<CandidateInfo> _repository;

    public SearchCandidateInfosRequestHandler(IReadRepository<CandidateInfo> repository) => _repository = repository;

    public async Task<PaginationResponse<CandidateInfoDto>> Handle(SearchCandidateInfosRequest request, CancellationToken cancellationToken)
    {
        var spec = new CandidateInfosBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}