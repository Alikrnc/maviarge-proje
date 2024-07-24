namespace FSH.WebApi.Domain.Catalog;

public class CandidateInfo : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public string? PhoneNo { get; private set; }
    public string? Email { get; private set; }
    public string? ImagePath { get; private set; }

    public CandidateInfo()
    {
        // Only needed for working with dapper (See GetProductViaDapperRequest)
        // If you're not using dapper it's better to remove this constructor.
    }
    public CandidateInfo(string name, string? description, string? phoneno, string? email, string? imagePath)
    {
        Name = name;
        Description = description;
        PhoneNo = phoneno;
        Email = email;
        ImagePath = imagePath;
    }

    public CandidateInfo Update(string? name, string? description, string? phoneNo, string? email, string? imagePath)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (phoneNo is not null && PhoneNo?.Equals(phoneNo) is not true) PhoneNo = phoneNo;
        if (email is not null && Email?.Equals(email) is not true) Email = email;
        if (imagePath is not null && ImagePath?.Equals(imagePath) is not true) ImagePath = imagePath;
        return this;
    }
    public CandidateInfo ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}