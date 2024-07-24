namespace FSH.WebApi.Application.Catalog.Interviews;

public class InterviewByNameSpec : Specification<Interview>, ISingleResultSpecification
{
    public InterviewByNameSpec(string name) =>
        Query.Where(n => n.Name == name);
}