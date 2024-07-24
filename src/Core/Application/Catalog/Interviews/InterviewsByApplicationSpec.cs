namespace FSH.WebApi.Application.Catalog.Interviews;

public class InterviewsByApplicationSpec : Specification<Interview>
{
    public InterviewsByApplicationSpec(Guid applicationId) =>
        Query.Where(a => a.ApplicationId == applicationId);
}
