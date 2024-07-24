using System;
using static System.Net.Mime.MediaTypeNames;

namespace FSH.WebApi.Domain.Catalog;

public class Interview : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public Guid ApplicationId { get; private set; }
    public virtual Application Application { get; private set; } = default!;

    public Interview()
    {
        // Only needed for working with dapper (See GetProductViaDapperRequest)
        // If you're not using dapper it's better to remove this constructor.
    }

    public Interview(string name, string? description, Guid applicationId)
    {
        Name = name;
        Description = description;
        ApplicationId = applicationId;
    }

    public Interview Update(string? name, string? description, Guid? applicationId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (applicationId.HasValue && applicationId.Value != Guid.Empty && !ApplicationId.Equals(applicationId.Value)) ApplicationId = applicationId.Value;
        return this;
    }
}