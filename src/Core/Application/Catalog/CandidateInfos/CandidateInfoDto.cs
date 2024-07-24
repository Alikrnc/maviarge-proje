namespace FSH.WebApi.Application.Catalog.CandidateInfos;

public class CandidateInfoDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? PhoneNo { get; set; }
    public string? Email { get; set; }
    public string? ImagePath { get; set; }
}