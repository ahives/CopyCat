using CopyCat.Data.Model;
using CopyCat.Model;
using CopyCat.Services;
using Microsoft.AspNetCore.Mvc;

namespace CopyCat.Endpoint.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountAdminController :
    ControllerBase
{
    private readonly IAccountAdminService _service;

    public AccountAdminController(IAccountAdminService service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetAccounts")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<Account>))]
    public IActionResult GetAccounts()
    {
        var result = _service.GetAllAccounts();
        
        return Ok(result.Data);
    }

    [HttpPost(Name = "CreateAccount")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult CreateAccount(CreateAccountRequest request)
    {
        var result = _service.CreateAccount(request);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [HttpPost(Name = "ActivateAccount")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Account))]
    public IActionResult ActivateAccount(Guid id)
    {
        var result = _service.ActivateAccount(id);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [HttpPost(Name = "DeactivateAccount")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Account))]
    public IActionResult DeactivateAccount(Guid id)
    {
        var result = _service.DeactivateAccount(id);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }
}