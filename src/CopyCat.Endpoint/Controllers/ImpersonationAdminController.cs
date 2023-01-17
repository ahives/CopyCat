using CopyCat.Data.Model;
using CopyCat.Model;
using CopyCat.Services;
using Microsoft.AspNetCore.Mvc;

namespace CopyCat.Endpoint.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ImpersonationAdminController :
    ControllerBase
{
    private readonly IImpersonatedAccountAdminService _service;

    public ImpersonationAdminController(IImpersonatedAccountAdminService service)
    {
        _service = service;
    }

    [HttpPost(Name = "CreateImpersonatedAccount")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult CreateImpersonatedAccount(CreateImpersonatedAccountRequest request)
    {
        var result = _service.CreateAccount(request);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [HttpGet(Name = "GetImpersonatedAccounts")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<ImpersonatedAccount>))]
    public IActionResult GetImpersonatedAccounts(Guid accountId)
    {
        var result = _service.GetAccounts(accountId);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [HttpGet(Name = "GetAllImpersonatedAccounts")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<ImpersonatedAccount>))]
    public IActionResult GetAllImpersonatedAccounts()
    {
        var result = _service.GetAllAccounts();

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [HttpGet(Name = "ActivateImpersonatedAccount")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImpersonatedAccount))]
    public IActionResult ActivateImpersonatedAccount(Guid id)
    {
        var result = _service.ActivateAccount(id);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [HttpGet(Name = "DeactivateImpersonatedAccount")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImpersonatedAccount))]
    public IActionResult DeactivateImpersonatedAccount(Guid id)
    {
        var result = _service.DeactivateAccount(id);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }
}