namespace FSH.WebApi.Application.Catalog.Applications;

public class SearchApplicantsRequest : PaginationFilter, IRequest<PaginationResponse<ApplicantDto>>
{
    public Guid? JobPostingId { get; set; }
}

public class SearchApplicantsRequestHandler : IRequestHandler<SearchApplicantsRequest, PaginationResponse<ApplicantDto>>
{
    private readonly IReadRepository<FSH.WebApi.Domain.Catalog.Application> _repository;

    public SearchApplicantsRequestHandler(IReadRepository<FSH.WebApi.Domain.Catalog.Application> repository) => _repository = repository;

    public async Task<PaginationResponse<ApplicantDto>> Handle(SearchApplicantsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ApplicantsBySearchRequestWithJobPostingsSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}