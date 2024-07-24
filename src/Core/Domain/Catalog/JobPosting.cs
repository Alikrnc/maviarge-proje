namespace FSH.WebApi.Domain.Catalog;

public class JobPosting : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    public JobPosting()
    {
        // Only needed for working with dapper (See GetProductViaDapperRequest)
        // If you're not using dapper it's better to remove this constructor.
    }
    public JobPosting(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public JobPosting Update(string? name, string? description)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        return this;
    }
}