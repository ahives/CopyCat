using CopyCat.Data.Model;
using CopyCat.Model;
using CopyCat.Services;
using Microsoft.AspNetCore.Mvc;

namespace CopyCat.Endpoint.Controllers;

[ApiController]
[Route("admin/account")]
public class AccountAdminController :
    ControllerBase
{
    private readonly IAccountAdminService _service;

    public AccountAdminController(IAccountAdminService service)
    {
        _service = service;
    }

    [Route("list-accounts")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<Account>))]
    public IActionResult GetAccounts()
    {
        var result = _service.GetAllAccounts();
        
        return Ok(result.Data);
    }

    [Route("create-account")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult CreateAccount(CreateAccountRequest request)
    {
        var result = _service.CreateAccount(request);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("activate-account")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Account))]
    public IActionResult ActivateAccount(Guid id)
    {
        var result = _service.ActivateAccount(id);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("deactivate-account")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Account))]
    public IActionResult DeactivateAccount(Guid id)
    {
        var result = _service.DeactivateAccount(id);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("update-account")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Account))]
    public IActionResult UpdateAccountName(Guid id, string name)
    {
        var result = _service.UpdateAccountName(id, name);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }
}