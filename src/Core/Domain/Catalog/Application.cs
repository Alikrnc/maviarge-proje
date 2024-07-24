namespace FSH.WebApi.Domain.Catalog;

public class Application : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public Guid JobPostingId { get; private set; }
    public Guid UserId { get; private set; }
    public virtual JobPosting JobPosting { get; private set; } = default!;
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;

    public Application()
    {
        // Only needed for working with dapper (See GetProductViaDapperRequest)
        // If you're not using dapper it's better to remove this constructor.
    }

    public Application(string name, string? description, Guid jobpostingId, Guid userId, string firstName, string lastName)
    {
        Name = name;
        Description = description;
        JobPostingId = jobpostingId;
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
    }

    public Application Update(string? name, string? description, Guid? jobpostingId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (jobpostingId.HasValue && jobpostingId.Value != Guid.Empty && !JobPostingId.Equals(jobpostingId.Value)) JobPostingId = jobpostingId.Value;
        return this;
    }
}