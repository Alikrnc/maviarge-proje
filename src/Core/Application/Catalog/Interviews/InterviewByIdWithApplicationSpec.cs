namespace FSH.WebApi.Application.Catalog.Interviews;

public class InterviewByIdWithApplicationSpec : Specification<Interview, InterviewDetailsDto>, ISingleResultSpecification
{
    public InterviewByIdWithApplicationSpec(Guid id) =>
        Query
            .Where(n => n.Id == id)
            .Include(n => n.Application);
}