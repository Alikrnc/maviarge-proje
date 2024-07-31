using System.Security.Claims;
using FSH.WebApi.Application.Common.Exceptions;
using System.Threading;
using FSH.WebApi.Application.Identity.Users;
using FSH.WebApi.Application.Identity.Users.Password;
using FSH.WebApi.Domain.Identity;
using FSH.WebApi.Shared.Events;
using Microsoft.AspNetCore.Identity;
using FSH.WebApi.Application.Catalog.Applications;
using DocumentFormat.OpenXml.Spreadsheet;

namespace FSH.WebApi.Host.Controllers.Identity;

public class PersonalController : VersionNeutralApiController
{
    private readonly IUserService _userService;
    private readonly IFileService _fileService;

    public PersonalController(IFileService fileService, IUserService userService)
    {
        _fileService = fileService;
        _userService = userService;
    }

    [HttpGet("profile")]
    [OpenApiOperation("Get profile details of currently logged in user.", "")]
    public async Task<ActionResult<UserDetailsDto>> GetProfileAsync(CancellationToken cancellationToken)
    {
        return User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
            ? Unauthorized()
            : Ok(await _userService.GetAsync(userId, cancellationToken));
    }

    [HttpPut("profile")]
    [OpenApiOperation("Update profile details of currently logged in user.", "")]
    public async Task<ActionResult> UpdateProfileAsync(UpdateUserRequest request)
    {
        if (User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        await _userService.UpdateAsync(request, userId);
        return Ok();
    }

    [HttpPut("profile-photo")]
    [OpenApiOperation("Update profile photo of currently logged in user.", "")]
    public async Task<ActionResult> UploadPhotoAsync([FromForm] UploadPhotoRequest request)
    {
        if (User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        await _userService.UploadPhotoAsync(request, userId);
        return Ok();
    }

    [HttpPut("change-password")]
    [OpenApiOperation("Change password of currently logged in user.", "")]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Register))]
    public async Task<ActionResult> ChangePasswordAsync(ChangePasswordRequest model)
    {
        if (User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        await _userService.ChangePasswordAsync(model, userId);
        return Ok();
    }

    [HttpGet("permissions")]
    [OpenApiOperation("Get permissions of currently logged in user.", "")]
    public async Task<ActionResult<List<string>>> GetPermissionsAsync(CancellationToken cancellationToken)
    {
        return User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
            ? Unauthorized()
            : Ok(await _userService.GetPermissionsAsync(userId, cancellationToken));
    }

    //[HttpGet("logs")]
    //[OpenApiOperation("Get audit logs of currently logged in user.", "")]
    //public Task<List<AuditDto>> GetLogsAsync()
    //{
    //    return Mediator.Send(new GetMyAuditLogsRequest());
    //}
}