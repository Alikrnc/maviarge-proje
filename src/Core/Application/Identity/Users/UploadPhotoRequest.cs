using Microsoft.AspNetCore.Http;

namespace FSH.WebApi.Application.Identity.Users;

public class UploadPhotoRequest
{
    public IFormFile Photo { get; set; } = default!;
    public string? PhotoPath { get; set; } = string.Empty;
}