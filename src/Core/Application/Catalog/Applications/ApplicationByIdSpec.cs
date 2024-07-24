namespace FSH.WebApi.Application.Catalog.Applications;

public class ApplicationByIdSpec : Specification<FSH.WebApi.Domain.Catalog.Application>, ISingleResultSpecification
{
    public ApplicationByIdSpec(Guid id) =>
        Query
        .Where(n => n.UserId == id);
}