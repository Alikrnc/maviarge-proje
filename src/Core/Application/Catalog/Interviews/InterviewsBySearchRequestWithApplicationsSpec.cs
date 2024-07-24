namespace FSH.WebApi.Application.Catalog.Interviews;

public class InterviewsBySearchRequestWithApplicationsSpec : EntitiesByPaginationFilterSpec<Interview, InterviewDto>
{
    public InterviewsBySearchRequestWithApplicationsSpec(SearchInterviewsRequest request)
        : base(request) =>
        Query
            .Include(a => a.Application)
            .OrderBy(n => n.Name, !request.HasOrderBy())
            .Where(a => a.ApplicationId.Equals(request.ApplicationId!.Value), request.ApplicationId.HasValue);
}