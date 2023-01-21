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
    public async Task<IActionResult> GetAccounts()
    {
        var result = await _service.GetAllAccounts();
        
        return Ok(result.Data);
    }

    [Route("get-account")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Account))]
    public async Task<IActionResult> GetAccount(Guid id)
    {
        var result = await _service.GetAccount(id);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("create-account")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAccount(AccountCreationRequest request)
    {
        var result = await _service.CreateAccount(request);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("activate-account")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Account))]
    public async Task<IActionResult> ActivateAccount(Guid id)
    {
        var result = await _service.ActivateAccount(id);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("deactivate-account")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Account))]
    public async Task<IActionResult> DeactivateAccount(Guid id)
    {
        var result = await _service.DeactivateAccount(id);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }

    [Route("update-account")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Account))]
    public async Task<IActionResult> UpdateAccountName(Guid id, string name)
    {
        var result = await _service.UpdateAccountName(id, name);

        if (!result.HasFaulted)
            return Ok(result.Data);

        return BadRequest();
    }
}