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
        var result = _service.CreateImpersonatedAccount(request);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [HttpGet(Name = "GetImpersonatedAccounts")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<ImpersonatedAccount>))]
    public IActionResult GetImpersonatedAccounts(Guid accountId)
    {
        var result = _service.GetImpersonatedAccounts(accountId);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [HttpGet(Name = "GetAllImpersonatedAccounts")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<ImpersonatedAccount>))]
    public IActionResult GetAllImpersonatedAccounts()
    {
        var result = _service.GetAllImpersonatedAccounts();

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }
}