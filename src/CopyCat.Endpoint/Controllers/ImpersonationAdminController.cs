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
    public IActionResult CreateImpersonatedAccount(CreateImpersonatedAccountRequest request)
    {
        var result = _service.CreateAccount(request);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("update-account-name")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImpersonatedAccount))]
    public IActionResult UpdateImpersonatedAccountName(Guid id, string name)
    {
        var result = _service.UpdateAccountName(id, name);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("update-client-identifier")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImpersonatedAccount))]
    public IActionResult UpdateImpersonatedAccountSendingClientId(Guid id, string sendingClientId)
    {
        var result = _service.UpdateSendingClientId(id, sendingClientId);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("update-facility-identifier")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImpersonatedAccount))]
    public IActionResult UpdateImpersonatedAccountSendingFacilityId(Guid id, string sendingFacilityId)
    {
        var result = _service.UpdateSendingFacilityId(id, sendingFacilityId);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("list-accounts")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<ImpersonatedAccount>))]
    public IActionResult GetImpersonatedAccounts(Guid accountId)
    {
        var result = _service.GetAccounts(accountId);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("list-all-account")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<ImpersonatedAccount>))]
    public IActionResult GetAllImpersonatedAccounts()
    {
        var result = _service.GetAllAccounts();

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("activate-account")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImpersonatedAccount))]
    public IActionResult ActivateImpersonatedAccount(Guid id)
    {
        var result = _service.ActivateAccount(id);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("deactivate-account")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImpersonatedAccount))]
    public IActionResult DeactivateImpersonatedAccount(Guid id)
    {
        var result = _service.DeactivateAccount(id);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }
}