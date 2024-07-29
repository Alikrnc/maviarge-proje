namespace FSH.WebApi.Application.Catalog.Applications;

public class ApplicantDto : IDto
{
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}