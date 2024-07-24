namespace FSH.WebApi.Application.Catalog.Applications;

public class ApplicationsBySearchRequestWithJobPostingsSpec : EntitiesByPaginationFilterSpec<FSH.WebApi.Domain.Catalog.Application, ApplicationDto>
{
    public ApplicationsBySearchRequestWithJobPostingsSpec(SearchApplicationsRequest request)
        : base(request) =>
        Query
            .Include(j => j.JobPosting)
            .Include(ci => ci.CandidateInfo)
            .OrderBy(n => n.Name, !request.HasOrderBy())
            .Where(j => j.JobPostingId.Equals(request.JobPostingId!.Value), request.JobPostingId.HasValue);
}