namespace FSH.WebApi.Application.Catalog.Applications;

public class ApplicationByNameSpec : Specification<FSH.WebApi.Domain.Catalog.Application>, ISingleResultSpecification
{
    public ApplicationByNameSpec(string name) =>
        Query
        .Where(n => n.Name == name);
}