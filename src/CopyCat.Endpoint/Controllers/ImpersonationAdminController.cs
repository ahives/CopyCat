using CopyCat.Data.Model;
using CopyCat.Model;
using CopyCat.Services;
using Microsoft.AspNetCore.Mvc;

namespace CopyCat.Endpoint.Controllers;

[ApiController]
[Route("admin/impersonation")]
public class ImpersonationAdminController :
    ControllerBase
{
    private readonly IImpersonatedAccountAdminService _service;

    public ImpersonationAdminController(IImpersonatedAccountAdminService service)
    {
        _service = service;
    }

    [Route("create-account")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImpersonatedAccount))]
    public async Task<IActionResult> CreateImpersonatedAccount(ImpersonatedAccountCreationRequest request)
    {
        var result = await _service.CreateAccount(request);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("update-account-name")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImpersonatedAccount))]
    public async Task<IActionResult> UpdateImpersonatedAccountName(Guid id, string name)
    {
        var result = await _service.UpdateAccountName(id, name);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("update-client-identifier")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImpersonatedAccount))]
    public async Task<IActionResult> UpdateImpersonatedAccountSendingClientId(Guid id, string sendingClientId)
    {
        var result = await _service.UpdateSendingClientId(id, sendingClientId);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("update-facility-identifier")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImpersonatedAccount))]
    public async Task<IActionResult> UpdateImpersonatedAccountSendingFacilityId(Guid id, string sendingFacilityId)
    {
        var result = await _service.UpdateSendingFacilityId(id, sendingFacilityId);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("list-accounts")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<ImpersonatedAccount>))]
    public async Task<IActionResult> GetImpersonatedAccounts(Guid accountId)
    {
        var result = await _service.GetAccounts(accountId);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("list-all-accounts")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<ImpersonatedAccount>))]
    public async Task<IActionResult> GetAllImpersonatedAccounts()
    {
        var result = await _service.GetAllAccounts();

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("get-account")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<ImpersonatedAccount>))]
    public async Task<IActionResult> GetAccount(Guid id)
    {
        var result = await _service.GetAccount(id);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("activate-account")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImpersonatedAccount))]
    public async Task<IActionResult> ActivateImpersonatedAccount(Guid id)
    {
        var result = await _service.ActivateAccount(id);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("deactivate-account")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImpersonatedAccount))]
    public async Task<IActionResult> DeactivateImpersonatedAccount(Guid id)
    {
        var result = await _service.DeactivateAccount(id);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }
}